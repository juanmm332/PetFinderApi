using EntitiesLayer;

namespace BusinessLogicLayer
{
    public interface IAnimalManager
    {
        List<Animal> GetAllAnimals();
        Animal? GetAnimalById(int id);
        string CreateAnimal(Animal animal);
        bool UpdateAnimal(int id, int userId, bool isAdmin, Animal animal);
        bool DeleteAnimal(int id, int userId, bool isAdmin);
        bool RestoreAnimal(int id);
    }
}