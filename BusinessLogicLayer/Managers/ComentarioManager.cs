
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

        string IComentarioManager.CreateComentario(Comentario comentario)
        {
            _Store.SaveComentario(comentario);
            return "Comentario creado exitosamente";
        }
    }
}
