using EntitiesLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public interface IAnimalStore
    {
        List<Animal> GetAllAnimals();
        Animal? GetAnimalById(int id);
        void SaveAnimal(Animal animal);
        bool UpdateAnimal(int id, int userId, bool isAdmin, Animal animal);
        bool DeleteAnimal(int id, int userId, bool isAdmin);
        bool RestoreAnimal(int id);
    }

    public class AnimalStore : IAnimalStore
    {
        private readonly ApplicationDbContext _context;

        public AnimalStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveAnimal(Animal animal)
        {
            _context.Animales.Add(animal);
            _context.SaveChanges();
        }

        public List<Animal> GetAllAnimals()
        {
            return _context.Animales
                .Include(animal => animal.Usuario)
                .Include(animal => animal.Avisos)
                .Where(animal => !animal.Eliminado)
                .AsNoTracking()
                .ToList();
        }

        public Animal? GetAnimalById(int id)
        {
            return _context.Animales
                .Include(animal => animal.Usuario)
                .AsNoTracking()
                .FirstOrDefault(animal => animal.Id == id && !animal.Eliminado);
        }

        public bool UpdateAnimal(int id, int userId, bool isAdmin, Animal updatedAnimal)
        {
            var animal = _context.Animales
                .FirstOrDefault(animal =>
                    animal.Id == id &&
                    !animal.Eliminado &&
                    (isAdmin || animal.IdUsuario == userId));
            if (animal is null)
            {
                return false;
            }

            animal.Nombre = updatedAnimal.Nombre;
            animal.Foto = updatedAnimal.Foto;
            animal.Comentario = updatedAnimal.Comentario;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteAnimal(int id, int userId, bool isAdmin)
        {
            var animal = _context.Animales
                .FirstOrDefault(animal =>
                    animal.Id == id &&
                    !animal.Eliminado &&
                    (isAdmin || animal.IdUsuario == userId));
            if (animal is null)
            {
                return false;
            }

            animal.Eliminado = true;
            _context.SaveChanges();
            return true;
        }

        public bool RestoreAnimal(int id)
        {
            var animal = _context.Animales
                .FirstOrDefault(animal => animal.Id == id && animal.Eliminado);
            if (animal is null)
            {
                return false;
            }

            animal.Eliminado = false;
            _context.SaveChanges();
            return true;
        }
    }
}