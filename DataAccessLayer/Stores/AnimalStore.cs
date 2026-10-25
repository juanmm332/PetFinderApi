using EntitiesLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public interface IAnimalStore
    {
        List<Animal> GetAllAnimals();
        void SaveAnimal(Animal animal);
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
                .AsNoTracking()
                .ToList();
        }
    }
}