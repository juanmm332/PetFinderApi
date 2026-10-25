using EntitiesLayer;

namespace BusinessLogicLayer
{
    public interface IAvisoManager
    {
        List<Aviso> GetAllAvisos();
        string CreateAviso(Aviso aviso);
    }
}