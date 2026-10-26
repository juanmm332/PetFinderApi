using EntitiesLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public interface IComentarioStore
    {
        List<Comentario> GetAllComentarios();
        Comentario? GetComentarioById(int id);
        void SaveComentario(Comentario comentario);
        bool UpdateComentario(int id, int userId, bool isAdmin, Comentario comentario);
        bool DeleteComentario(int id, int userId, bool isAdmin);
        bool RestoreComentario(int id);
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

        public Comentario? GetComentarioById(int id)
        {
            return _context.Comentarios
                .Include(comentario => comentario.Usuario)
                .Include(comentario => comentario.Aviso)
                .AsNoTracking()
                .FirstOrDefault(comentario => comentario.Id == id && !comentario.Eliminado);
        }

        public bool UpdateComentario(int id, int userId, bool isAdmin, Comentario updatedComentario)
        {
            var comentario = _context.Comentarios
                .FirstOrDefault(comentario =>
                    comentario.Id == id &&
                    !comentario.Eliminado &&
                    (isAdmin || comentario.IdUsuario == userId));
            if (comentario is null)
            {
                return false;
            }

            comentario.Texto = updatedComentario.Texto;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteComentario(int id, int userId, bool isAdmin)
        {
            var comentario = _context.Comentarios
                .FirstOrDefault(comentario =>
                    comentario.Id == id &&
                    !comentario.Eliminado &&
                    (isAdmin || comentario.IdUsuario == userId));
            if (comentario is null)
            {
                return false;
            }

            comentario.Eliminado = true;
            _context.SaveChanges();
            return true;
        }

        public bool RestoreComentario(int id)
        {
            var comentario = _context.Comentarios
                .FirstOrDefault(comentario => comentario.Id == id && comentario.Eliminado);
            if (comentario is null)
            {
                return false;
            }

            comentario.Eliminado = false;
            _context.SaveChanges();
            return true;
        }
    }
}