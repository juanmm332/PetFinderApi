using EntitiesLayer;

namespace BusinessLogicLayer
{
    public interface IAnimalManager
    {
        List<Animal> GetAllAnimals();
        string CreateAnimal(Animal animal);
    }
}