using EntitiesLayer;

namespace BusinessLogicLayer
{
    public interface IComentarioManager
    {
        List<Comentario> GetAllComentarios();
        Comentario? GetComentarioById(int id);
        string CreateComentario(Comentario comentario);
        bool UpdateComentario(int id, int userId, bool isAdmin, Comentario comentario);
        bool DeleteComentario(int id, int userId, bool isAdmin);
        bool RestoreComentario(int id);
    }
}