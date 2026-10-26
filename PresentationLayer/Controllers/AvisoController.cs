using BusinessLogicLayer;
using EntitiesLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvisoController : ControllerBase
    {
        private readonly IAvisoManager _manager;
        
        public AvisoController(IAvisoManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var avisoList = _manager.GetAllAvisos()
                .Select(aviso => new
                {
                    aviso.Id,
                    aviso.IdAnimal,
                    Estado = aviso.Estado.ToString(),
                    aviso.UltimaUbicacionVista,
                    aviso.Comentario,
                    aviso.Fecha
                });

            return Ok(new { avisoList });
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aviso = _manager.GetAvisoById(id);
            if (aviso is null)
            {
                return NotFound();
            }

            return Ok(new
            {
                aviso.Id,
                aviso.IdAnimal,
                Estado = aviso.Estado.ToString(),
                aviso.UltimaUbicacionVista,
                aviso.Comentario,
                aviso.Fecha
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateAvisoRequest request)
        {
            var aviso = new Aviso
            {
                IdAnimal = request.IdAnimal,
                Estado = request.Estado,
                UltimaUbicacionVista = request.UltimaUbicacionVista,
                Comentario = request.Comentario,
                Fecha = DateTime.UtcNow
            };
            string result = _manager.CreateAviso(aviso);

            return Ok(new { Message = result });
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] UpdateAvisoRequest request)
        {
            var aviso = new Aviso
            {
                Estado = request.Estado,
                UltimaUbicacionVista = request.UltimaUbicacionVista,
                Comentario = request.Comentario
            };

            return _manager.UpdateAviso(id, 0, true, aviso)
                ? NoContent()
                : NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return _manager.DeleteAviso(id, 0, true)
                ? NoContent()
                : NotFound();
        }
    }
}
