using EntitiesLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public interface IAvisoStore
    {
        List<Aviso> GetAllAvisos();
        void SaveAviso(Aviso aviso);
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
    }
}