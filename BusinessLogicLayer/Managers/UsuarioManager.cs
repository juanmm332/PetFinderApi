
using DataAccessLayer;
using EntitiesLayer;

namespace BusinessLogicLayer
{
    public class UsuarioManager: IUsuarioManager
    {
        private readonly IUsuarioStore _Store;

        public UsuarioManager(IUsuarioStore store)
        {
            _Store = store;
        }


        List<Usuario> IUsuarioManager.GetAllUsuarios()
        {
            return _Store.GetAllUsuarios();
        }

        Usuario? IUsuarioManager.GetUsuarioById(int id)
        {
            return _Store.GetUsuarioById(id);
        }

        Usuario? IUsuarioManager.GetUsuarioByCorreo(string correo)
        {
            return _Store.GetUsuarioByCorreo(correo);
        }

        bool IUsuarioManager.IsCorreoInUse(string correo, int? exceptUserId)
        {
            return _Store.IsCorreoInUse(correo, exceptUserId);
        }

        string IUsuarioManager.CreateUsuario(Usuario usuario)
        {
            _Store.SaveUsuario(usuario);
            return "Usuario creado exitosamente";
        }

        bool IUsuarioManager.UpdateUsuario(int id, int userId, bool isAdmin, Usuario usuario)
        {
            return _Store.UpdateUsuario(id, userId, isAdmin, usuario);
        }

        bool IUsuarioManager.DeleteUsuario(int id, int userId, bool isAdmin)
        {
            return _Store.DeleteUsuario(id, userId, isAdmin);
        }

        bool IUsuarioManager.RestoreUsuario(int id)
        {
            return _Store.RestoreUsuario(id);
        }

        void IUsuarioManager.UpdatePasswordHash(int id, string passwordHash)
        {
            _Store.UpdatePasswordHash(id, passwordHash);
        }
    }
}
