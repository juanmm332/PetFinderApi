using BusinessLogicLayer;
using EntitiesLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioManager _manager;
        
        public ComentarioController(IComentarioManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var comentarioList = _manager.GetAllComentarios()
                .Select(comentario => new
                {
                    comentario.Id,
                    comentario.Texto,
                    comentario.IdAviso,
                    comentario.IdUsuario,
                    Usuario = comentario.Usuario is null
                        ? null
                        : new
                        {
                            comentario.Usuario.Id,
                            comentario.Usuario.Nombre,
                            comentario.Usuario.Correo,
                            comentario.Usuario.Telefono
                        },
                    Aviso = comentario.Aviso is null
                        ? null
                        : new
                        {
                            comentario.Aviso.Id,
                            comentario.Aviso.Estado,
                            comentario.Aviso.UltimaUbicacionVista
                        }
                });

            return Ok(new { comentarioList });
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var comentario = _manager.GetComentarioById(id);
            if (comentario is null)
            {
                return NotFound();
            }

            return Ok(new
            {
                comentario.Id,
                comentario.Texto,
                comentario.IdAviso,
                comentario.IdUsuario,
                comentario.Fecha
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateComentarioRequest request)
        {
            var comentario = new Comentario
            {
                Texto = request.Texto,
                IdAviso = request.IdAviso,
                IdUsuario = request.IdUsuario,
                Fecha = DateTime.UtcNow
            };
            string result = _manager.CreateComentario(comentario);

            return Ok(new { Message = result });
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] UpdateComentarioRequest request)
        {
            var comentario = new Comentario
            {
                Texto = request.Texto
            };

            return _manager.UpdateComentario(id, 0, true, comentario)
                ? NoContent()
                : NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return _manager.DeleteComentario(id, 0, true)
                ? NoContent()
                : NotFound();
        }
    }
}
