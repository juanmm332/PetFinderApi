using BusinessLogicLayer;
using EntitiesLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalManager _manager;
        
        public AnimalController(IAnimalManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var animalList = _manager.GetAllAnimals()
                .Select(animal => new
                {
                    animal.Id,
                    animal.Nombre,
                    animal.Foto,
                    animal.Comentario,
                    animal.IdUsuario,
                    Usuario = animal.Usuario is null
                        ? null
                        : new
                        {
                            animal.Usuario.Id,
                            animal.Usuario.Nombre,
                            animal.Usuario.Correo,
                            animal.Usuario.Telefono
                        }
                });

            return Ok(new { animalList });
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateAnimalRequest request)
        {
            var animal = new Animal
            {
                Nombre = request.Nombre,
                Foto = request.Foto,
                Comentario = request.Comentario,
                IdUsuario = request.IdUsuario
            };
            string result = _manager.CreateAnimal(animal);

            return Ok(new { Message = result });
        }
    }
}
