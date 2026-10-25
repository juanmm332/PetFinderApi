
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

        string IAvisoManager.CreateAviso(Aviso aviso)
        {
            _Store.SaveAviso(aviso);
            return "Aviso creado exitosamente";
        }
    }
}
