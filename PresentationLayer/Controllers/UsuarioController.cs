using BusinessLogicLayer;
using EntitiesLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace PresentationLayer
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : AuthorizedApiController
    {
        private readonly IUsuarioManager _manager;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public UsuarioController(IUsuarioManager manager, IPasswordHasher<Usuario> passwordHasher)
        {
            _manager = manager;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var usuarioList = _manager.GetAllUsuarios().Select(ToResponse);
            return Ok(new { usuarioList });
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var usuario = _manager.GetUsuarioById(id);
            return usuario is null ? NotFound() : Ok(ToResponse(usuario));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] CreateUsuarioRequest request)
        {
            var correo = request.Correo.Trim().ToLowerInvariant();
            if (_manager.IsCorreoInUse(correo))
            {
                return Conflict(new { Message = "Ya existe una cuenta con ese correo." });
            }

            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Contraseña = string.Empty,
                Correo = correo,
                Telefono = request.Telefono,
                EsAdministrador = false
            };
            usuario.Contraseña = _passwordHasher.HashPassword(usuario, request.Contraseña);
            try
            {
                _manager.CreateUsuario(usuario);
            }
            catch (DbUpdateException exception) when (IsDuplicateKey(exception))
            {
                return Conflict(new { Message = "Ya existe una cuenta con ese correo." });
            }

            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, ToResponse(usuario));
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(
            int id,
            [FromBody] UpdateUsuarioRequest request)
        {
            var userId = CurrentUserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var correo = request.Correo.Trim().ToLowerInvariant();
            if (_manager.IsCorreoInUse(correo, id))
            {
                return Conflict(new { Message = "Ya existe una cuenta con ese correo." });
            }

            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Contraseña = string.Empty,
                Correo = correo,
                Telefono = request.Telefono,
                EsAdministrador = false
            };
            usuario.Contraseña = _passwordHasher.HashPassword(usuario, request.Contraseña);
            try
            {
                return _manager.UpdateUsuario(id, userId.Value, IsAdministrator, usuario)
                    ? NoContent()
                    : NotFound();
            }
            catch (DbUpdateException exception) when (IsDuplicateKey(exception))
            {
                return Conflict(new { Message = "Ya existe una cuenta con ese correo." });
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var userId = CurrentUserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            return _manager.DeleteUsuario(id, userId.Value, IsAdministrator) ? NoContent() : NotFound();
        }

        [HttpPatch("{id:int}/restore")]
        [Authorize(Roles = "Admin")]
        public IActionResult Restore(int id)
        {
            return _manager.RestoreUsuario(id) ? NoContent() : NotFound();
        }

        private static object ToResponse(Usuario usuario)
        {
            return new
            {
                IdUsuario = usuario.Id,
                usuario.Id,
                usuario.Nombre,
                usuario.Correo,
                usuario.Telefono,
                usuario.EsAdministrador
            };
        }

        private static bool IsDuplicateKey(DbUpdateException exception)
        {
            return exception.InnerException is MySqlException { Number: 1062 };
        }

    }
}
