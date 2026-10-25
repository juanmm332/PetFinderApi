
using DataAccessLayer;
using EntitiesLayer;

namespace BusinessLogicLayer
{
    public class AnimalManager: IAnimalManager
    {
        private readonly IAnimalStore _Store;

        public AnimalManager(IAnimalStore store)
        {
            _Store = store;
        }


        List<Animal> IAnimalManager.GetAllAnimals()
        {
            return _Store.GetAllAnimals();
        }

        string IAnimalManager.CreateAnimal(Animal animal)
        {
            _Store.SaveAnimal(animal);
            return "Animal creado exitosamente";
        }
    }
}
