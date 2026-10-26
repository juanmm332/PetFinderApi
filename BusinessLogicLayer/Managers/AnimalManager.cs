
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

        Animal? IAnimalManager.GetAnimalById(int id)
        {
            return _Store.GetAnimalById(id);
        }

        string IAnimalManager.CreateAnimal(Animal animal)
        {
            _Store.SaveAnimal(animal);
            return "Animal creado exitosamente";
        }

        bool IAnimalManager.UpdateAnimal(int id, int userId, bool isAdmin, Animal animal)
        {
            return _Store.UpdateAnimal(id, userId, isAdmin, animal);
        }

        bool IAnimalManager.DeleteAnimal(int id, int userId, bool isAdmin)
        {
            return _Store.DeleteAnimal(id, userId, isAdmin);
        }

        bool IAnimalManager.RestoreAnimal(int id)
        {
            return _Store.RestoreAnimal(id);
        }
    }
}
