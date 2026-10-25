using BusinessLogicLayer;
using EntitiesLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioManager _manager;
        
        public UsuarioController(IUsuarioManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            List<Usuario> usuarioList = _manager.GetAllUsuarios();
            return Ok(new { usuarioList });
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUsuarioRequest request)
        {
            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Contraseña = request.Contraseña,
                Correo = request.Correo,
                Telefono = request.Telefono
            };
            string result = _manager.CreateUsuario(usuario);

            return Ok(new { Message = result });
        }
    }
}
