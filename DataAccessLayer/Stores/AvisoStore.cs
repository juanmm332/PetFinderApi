using EntitiesLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public interface IAvisoStore
    {
        List<Aviso> GetAllAvisos();
        Aviso? GetAvisoById(int id);
        void SaveAviso(Aviso aviso);
        bool UpdateAviso(int id, int userId, bool isAdmin, Aviso aviso);
        bool DeleteAviso(int id, int userId, bool isAdmin);
        bool RestoreAviso(int id);
    }

    public class AvisoStore : IAvisoStore
    {
        private readonly ApplicationDbContext _context;

        public AvisoStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveAviso(Aviso aviso)
        {
            _context.Avisos.Add(aviso);
            _context.SaveChanges();
        }

        public List<Aviso> GetAllAvisos()
        {
            return _context.Avisos.Where(aviso => !aviso.Eliminado)
            .Include(aviso => aviso.Animal)
            .Include(aviso => aviso.Comentarios)
            .AsNoTracking().ToList();
        }

        public Aviso? GetAvisoById(int id)
        {
            return _context.Avisos
                .Include(aviso => aviso.Animal)
                .AsNoTracking()
                .FirstOrDefault(aviso => aviso.Id == id && !aviso.Eliminado);
        }

        public bool UpdateAviso(int id, int userId, bool isAdmin, Aviso updatedAviso)
        {
            var aviso = _context.Avisos
                .Include(aviso => aviso.Animal)
                .FirstOrDefault(aviso =>
                    aviso.Id == id &&
                    !aviso.Eliminado &&
                    (isAdmin || aviso.Animal!.IdUsuario == userId));
            if (aviso is null)
            {
                return false;
            }

            aviso.Estado = updatedAviso.Estado;
            aviso.UltimaUbicacionVista = updatedAviso.UltimaUbicacionVista;
            aviso.Comentario = updatedAviso.Comentario;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteAviso(int id, int userId, bool isAdmin)
        {
            var aviso = _context.Avisos
                .Include(aviso => aviso.Animal)
                .FirstOrDefault(aviso =>
                    aviso.Id == id &&
                    !aviso.Eliminado &&
                    (isAdmin || aviso.Animal!.IdUsuario == userId));
            if (aviso is null)
            {
                return false;
            }

            aviso.Eliminado = true;
            _context.SaveChanges();
            return true;
        }

        public bool RestoreAviso(int id)
        {
            var aviso = _context.Avisos
                .FirstOrDefault(aviso => aviso.Id == id && aviso.Eliminado);
            if (aviso is null)
            {
                return false;
            }

            aviso.Eliminado = false;
            _context.SaveChanges();
            return true;
        }
    }
}