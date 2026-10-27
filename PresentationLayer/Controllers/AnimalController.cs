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
    public class AnimalController : AuthorizedApiController
    {
        private readonly IAnimalManager _manager;
        
        public AnimalController(IAnimalManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var animalList = _manager.GetAllAnimals().Select(ToResponse);
            return Ok(new { animalList });
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var animal = _manager.GetAnimalById(id);
            return animal is null ? NotFound() : Ok(ToResponse(animal));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateAnimalRequest request)
        {
            var userId = CurrentUserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var animal = new Animal
            {
                Nombre = request.Nombre,
                Foto = request.Foto,
                Comentario = request.Comentario,
                IdUsuario = userId.Value
            };
            _manager.CreateAnimal(animal);
            return CreatedAtAction(nameof(GetById), new { id = animal.Id }, ToResponse(animal));
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] UpdateAnimalRequest request)
        {
            var userId = CurrentUserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            var animal = new Animal
            {
                Nombre = request.Nombre,
                Foto = request.Foto,
                Comentario = request.Comentario
            };
            return _manager.UpdateAnimal(id, userId.Value, IsAdministrator, animal) ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var userId = CurrentUserId;
            if (userId is null)
            {
                return Unauthorized();
            }

            return _manager.DeleteAnimal(id, userId.Value, IsAdministrator) ? NoContent() : NotFound();
        }

        [HttpPatch("{id:int}/restore")]
        [Authorize(Roles = "Admin")]
        public IActionResult Restore(int id)
        {
            return _manager.RestoreAnimal(id) ? NoContent() : NotFound();
        }

        private static object ToResponse(Animal animal)
        {
            return new
            {
                IdAnimal = animal.Id,
                animal.Id,
                animal.Nombre,
                animal.Foto,
                animal.Comentario,
                animal.IdUsuario,
                Usuario = animal.Usuario is null || animal.Usuario.Eliminado
                    ? null
                    : new
                    {
                        IdUsuario = animal.Usuario.Id,
                        animal.Usuario.Id,
                        animal.Usuario.Nombre,
                        animal.Usuario.Correo,
                        animal.Usuario.Telefono
                    }
            };
        }

    }
}
