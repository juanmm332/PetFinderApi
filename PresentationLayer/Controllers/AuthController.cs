using System.Security.Cryptography;
using System.Text;
using EntitiesLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Security;
using BusinessLogicLayer;

namespace PresentationLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AuthController : ControllerBase
    {
        private readonly IUsuarioManager _usuarios;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly IJwtTokenService _tokenService;

        public AuthController(
            IUsuarioManager usuarios,
            IPasswordHasher<Usuario> passwordHasher,
            IJwtTokenService tokenService)
        {
            _usuarios = usuarios;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var usuario = _usuarios.GetUsuarioByCorreo(request.Correo.Trim().ToLowerInvariant());
            if (usuario is null || !VerifyAndUpgradePassword(usuario, request.Contraseña))
            {
                return Unauthorized(new { Message = "Correo o contraseña incorrectos." });
            }

            return Ok(new LoginResponse(
                _tokenService.CreateToken(usuario),
                "Bearer",
                DateTimeOffset.UtcNow.AddHours(1)));
        }

        private bool VerifyAndUpgradePassword(Usuario usuario, string password)
        {
            if (string.IsNullOrEmpty(usuario.Contraseña))
            {
                return false;
            }

            PasswordVerificationResult verification;
            try
            {
                verification = _passwordHasher.VerifyHashedPassword(
                    usuario,
                    usuario.Contraseña,
                    password);
            }
            catch (FormatException)
            {
                verification = PasswordVerificationResult.Failed;
            }

            if (verification == PasswordVerificationResult.SuccessRehashNeeded)
            {
                _usuarios.UpdatePasswordHash(
                    usuario.Id,
                    _passwordHasher.HashPassword(usuario, password));
                return true;
            }

            if (verification == PasswordVerificationResult.Success)
            {
                return true;
            }

            // Existing accounts may still have a legacy plaintext password.
            var expected = Encoding.UTF8.GetBytes(usuario.Contraseña);
            var provided = Encoding.UTF8.GetBytes(password);
            if (!CryptographicOperations.FixedTimeEquals(expected, provided))
            {
                return false;
            }

            _usuarios.UpdatePasswordHash(usuario.Id, _passwordHasher.HashPassword(usuario, password));
            return true;
        }
    }
}
