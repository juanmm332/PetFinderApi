using EntitiesLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public interface IUsuarioStore
    {
        List<Usuario> GetAllUsuarios();
        void SaveUsuario(Usuario usuario);
    }

    public class UsuarioStore : IUsuarioStore
    {
        private readonly ApplicationDbContext _context;

        public UsuarioStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public List<Usuario> GetAllUsuarios()
        {
            return _context.Usuarios
                .Where(usuario => !usuario.Eliminado)
                .AsNoTracking()
                .ToList();
        }
    }
}
