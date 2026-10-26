
using DataAccessLayer;
using EntitiesLayer;

namespace BusinessLogicLayer
{
    public class AvisoManager: IAvisoManager
    {
        private readonly IAvisoStore _Store;

        public AvisoManager(IAvisoStore store)
        {
            _Store = store;
        }


        List<Aviso> IAvisoManager.GetAllAvisos()
        {
            return _Store.GetAllAvisos();
        }

        Aviso? IAvisoManager.GetAvisoById(int id)
        {
            return _Store.GetAvisoById(id);
        }

        string IAvisoManager.CreateAviso(Aviso aviso)
        {
            _Store.SaveAviso(aviso);
            return "Aviso creado exitosamente";
        }

        bool IAvisoManager.UpdateAviso(int id, int userId, bool isAdmin, Aviso aviso)
        {
            return _Store.UpdateAviso(id, userId, isAdmin, aviso);
        }

        bool IAvisoManager.DeleteAviso(int id, int userId, bool isAdmin)
        {
            return _Store.DeleteAviso(id, userId, isAdmin);
        }

        bool IAvisoManager.RestoreAviso(int id)
        {
            return _Store.RestoreAviso(id);
        }
    }
}
