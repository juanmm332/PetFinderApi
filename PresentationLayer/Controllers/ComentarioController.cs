using BusinessLogicLayer;
using EntitiesLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Security;
using Microsoft.AspNetCore.Authorization;

namespace PresentationLayer
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComentarioController : AuthorizedApiController
    {
        private readonly IComentarioManager _manager;
        
        public ComentarioController(IComentarioManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var comentarioList = _manager.GetAllComentarios().Select(ToResponse);
            return Ok(new { comentarioList });
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var comentario = _manager.GetComentarioById(id);
            return comentario is null ? NotFound() : Ok(ToResponse(comentario));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateComentarioRequest request)
        {
            var userId = CurrentUserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var comentario = new Comentario
            {
                Texto = request.Texto,
                IdAviso = request.IdAviso,
                IdUsuario = userId.Value,
                Fecha = DateTime.UtcNow
            };
            _manager.CreateComentario(comentario);
            return CreatedAtAction(nameof(GetById), new { id = comentario.Id }, ToResponse(comentario));
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] UpdateComentarioRequest request)
        {
            var userId = CurrentUserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var comentario = new Comentario
            {
                Texto = request.Texto
            };
            return _manager.UpdateComentario(id, userId.Value, IsAdministrator, comentario)
                ? NoContent()
                : NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var userId = CurrentUserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            return _manager.DeleteComentario(id, userId.Value, IsAdministrator) ? NoContent() : NotFound();
        }

        [HttpPatch("{id:int}/restore")]
        [Authorize(Roles = "Admin")]
        public IActionResult Restore(int id)
        {
            return _manager.RestoreComentario(id) ? NoContent() : NotFound();
        }

        private static object ToResponse(Comentario comentario)
        {
            return new
            {
                comentario.Id,
                comentario.Texto,
                comentario.IdAviso,
                comentario.IdUsuario,
                Usuario = comentario.Usuario is null || comentario.Usuario.Eliminado
                    ? null
                    : new
                    {
                        comentario.Usuario.Id,
                        comentario.Usuario.Nombre,
                        comentario.Usuario.Correo,
                        comentario.Usuario.Telefono
                    },
                Aviso = comentario.Aviso is null || comentario.Aviso.Eliminado
                    ? null
                    : new
                    {
                        comentario.Aviso.Id,
                        Estado = comentario.Aviso.Estado.ToString(),
                        comentario.Aviso.UltimaUbicacionVista
                    }
            };
        }

    }
}
