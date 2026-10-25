using EntitiesLayer;

namespace BusinessLogicLayer
{
    public interface IComentarioManager
    {
        List<Comentario> GetAllComentarios();
        string CreateComentario(Comentario comentario);
    }
}