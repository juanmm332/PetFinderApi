
using DataAccessLayer;
using EntitiesLayer;

namespace BusinessLogicLayer
{
    public class ComentarioManager: IComentarioManager
    {
        private readonly IComentarioStore _Store;

        public ComentarioManager(IComentarioStore store)
        {
            _Store = store;
        }


        List<Comentario> IComentarioManager.GetAllComentarios()
        {
            return _Store.GetAllComentarios();
        }

        Comentario? IComentarioManager.GetComentarioById(int id)
        {
            return _Store.GetComentarioById(id);
        }

        string IComentarioManager.CreateComentario(Comentario comentario)
        {
            _Store.SaveComentario(comentario);
            return "Comentario creado exitosamente";
        }

        bool IComentarioManager.UpdateComentario(int id, int userId, bool isAdmin, Comentario comentario)
        {
            return _Store.UpdateComentario(id, userId, isAdmin, comentario);
        }

        bool IComentarioManager.DeleteComentario(int id, int userId, bool isAdmin)
        {
            return _Store.DeleteComentario(id, userId, isAdmin);
        }

        bool IComentarioManager.RestoreComentario(int id)
        {
            return _Store.RestoreComentario(id);
        }
    }
}
