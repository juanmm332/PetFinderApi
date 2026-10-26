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
    public class AvisoController : AuthorizedApiController
    {
        private readonly IAvisoManager _manager;
        private readonly IAnimalManager _animals;

        public AvisoController(IAvisoManager manager, IAnimalManager animals)
        {
            _manager = manager;
            _animals = animals;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var avisoList = _manager.GetAllAvisos().Select(ToResponse);
            return Ok(new { avisoList });
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aviso = _manager.GetAvisoById(id);
            return aviso is null ? NotFound() : Ok(ToResponse(aviso));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateAvisoRequest request)
        {
            var userId = CurrentUserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var animal = _animals.GetAnimalById(request.IdAnimal);
            if (animal is null || (!IsAdministrator && animal.IdUsuario != userId.Value))
            {
                return NotFound();
            }

            var aviso = new Aviso
            {
                IdAnimal = request.IdAnimal,
                Estado = request.Estado,
                UltimaUbicacionVista = request.UltimaUbicacionVista,
                Comentario = request.Comentario,
                Fecha = DateTime.UtcNow
            };
            _manager.CreateAviso(aviso);
            return CreatedAtAction(nameof(GetById), new { id = aviso.Id }, ToResponse(aviso));
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] UpdateAvisoRequest request)
        {
            var userId = CurrentUserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var aviso = new Aviso
            {
                Estado = request.Estado,
                UltimaUbicacionVista = request.UltimaUbicacionVista,
                Comentario = request.Comentario
            };
            return _manager.UpdateAviso(id, userId.Value, IsAdministrator, aviso) ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var userId = CurrentUserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            return _manager.DeleteAviso(id, userId.Value, IsAdministrator) ? NoContent() : NotFound();
        }

        [HttpPatch("{id:int}/restore")]
        [Authorize(Roles = "Admin")]
        public IActionResult Restore(int id)
        {
            return _manager.RestoreAviso(id) ? NoContent() : NotFound();
        }

        private static object ToResponse(Aviso aviso)
        {
            return new
            {
                aviso.Id,
                aviso.IdAnimal,
                Estado = aviso.Estado.ToString(),
                aviso.UltimaUbicacionVista,
                aviso.Comentario,
                aviso.Fecha
            };
        }

    }
}
