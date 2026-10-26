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

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var animal = _manager.GetAnimalById(id);
            if (animal is null)
            {
                return NotFound();
            }

            return Ok(new
            {
                animal.Id,
                animal.Nombre,
                animal.Foto,
                animal.Comentario,
                animal.IdUsuario
            });
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

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] UpdateAnimalRequest request)
        {
            var animal = new Animal
            {
                Nombre = request.Nombre,
                Foto = request.Foto,
                Comentario = request.Comentario
            };

            return _manager.UpdateAnimal(id, 0, true, animal)
                ? NoContent()
                : NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return _manager.DeleteAnimal(id, 0, true)
                ? NoContent()
                : NotFound();
        }
    }
}
