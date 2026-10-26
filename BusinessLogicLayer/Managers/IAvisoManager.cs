using EntitiesLayer;

namespace BusinessLogicLayer
{
    public interface IAvisoManager
    {
        List<Aviso> GetAllAvisos();
        Aviso? GetAvisoById(int id);
        string CreateAviso(Aviso aviso);
        bool UpdateAviso(int id, int userId, bool isAdmin, Aviso aviso);
        bool DeleteAviso(int id, int userId, bool isAdmin);
        bool RestoreAviso(int id);
    }
}