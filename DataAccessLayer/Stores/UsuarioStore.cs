using EntitiesLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public interface IUsuarioStore
    {
        List<Usuario> GetAllUsuarios();
        Usuario? GetUsuarioById(int id);
        Usuario? GetUsuarioByCorreo(string correo);
        void SaveUsuario(Usuario usuario);
        bool UpdateUsuario(int id, int userId, bool isAdmin, Usuario usuario);
        bool DeleteUsuario(int id, int userId, bool isAdmin);
        bool RestoreUsuario(int id);
        void UpdatePasswordHash(int id, string passwordHash);
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

        public Usuario? GetUsuarioById(int id)
        {
            return _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(usuario => usuario.Id == id && !usuario.Eliminado);
        }

        public Usuario? GetUsuarioByCorreo(string correo)
        {
            return _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(usuario =>
                    usuario.Correo == correo && !usuario.Eliminado);
        }

        public bool UpdateUsuario(int id, int userId, bool isAdmin, Usuario updatedUsuario)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(usuario =>
                    usuario.Id == id &&
                    !usuario.Eliminado &&
                    (isAdmin || usuario.Id == userId));
            if (usuario is null)
            {
                return false;
            }

            usuario.Nombre = updatedUsuario.Nombre;
            usuario.Contraseña = updatedUsuario.Contraseña;
            usuario.Correo = updatedUsuario.Correo;
            usuario.Telefono = updatedUsuario.Telefono;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUsuario(int id, int userId, bool isAdmin)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(usuario =>
                    usuario.Id == id &&
                    !usuario.Eliminado &&
                    (isAdmin || usuario.Id == userId));
            if (usuario is null)
            {
                return false;
            }

            usuario.Eliminado = true;
            _context.SaveChanges();
            return true;
        }

        public bool RestoreUsuario(int id)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(usuario => usuario.Id == id && usuario.Eliminado);
            if (usuario is null)
            {
                return false;
            }

            usuario.Eliminado = false;
            _context.SaveChanges();
            return true;
        }

        public void UpdatePasswordHash(int id, string passwordHash)
        {
            var usuario = _context.Usuarios.Find(id)
                ?? throw new InvalidOperationException($"No se encontró el usuario {id}.");
            usuario.Contraseña = passwordHash;
            _context.SaveChanges();
        }
    }
}
