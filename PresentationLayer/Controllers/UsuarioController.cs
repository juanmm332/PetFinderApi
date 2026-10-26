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

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var usuario = _manager.GetUsuarioById(id);
            if (usuario is null)
            {
                return NotFound();
            }

            return Ok(new
            {
                usuario.Id,
                usuario.Nombre,
                usuario.Correo,
                usuario.Telefono,
                usuario.EsAdministrador
            });
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

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] UpdateUsuarioRequest request)
        {
            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Contraseña = request.Contraseña,
                Correo = request.Correo,
                Telefono = request.Telefono
            };

            return _manager.UpdateUsuario(id, 0, true, usuario)
                ? NoContent()
                : NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return _manager.DeleteUsuario(id, 0, true)
                ? NoContent()
                : NotFound();
        }
    }
}
