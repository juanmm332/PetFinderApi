using EntitiesLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public interface IComentarioStore
    {
        List<Comentario> GetAllComentarios();
        void SaveComentario(Comentario comentario);
    }
    public class ComentarioStore : IComentarioStore
    {
        private readonly ApplicationDbContext _context;

        public ComentarioStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveComentario(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            _context.SaveChanges();
        }

        public List<Comentario> GetAllComentarios()
        {
            return _context.Comentarios
                .Where(comentario => !comentario.Eliminado)
                .Include(comentario => comentario.Usuario)
                .Include(comentario => comentario.Aviso)
                .AsNoTracking()
                .ToList();
        }
    }
}