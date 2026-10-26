using EntitiesLayer;

namespace BusinessLogicLayer
{
    public interface IUsuarioManager
    {
        List<Usuario> GetAllUsuarios();
        Usuario? GetUsuarioById(int id);
        Usuario? GetUsuarioByCorreo(string correo);
        string CreateUsuario(Usuario usuario);
        bool UpdateUsuario(int id, int userId, bool isAdmin, Usuario usuario);
        bool DeleteUsuario(int id, int userId, bool isAdmin);
        bool RestoreUsuario(int id);
        void UpdatePasswordHash(int id, string passwordHash);
    }
}