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
    }
}
