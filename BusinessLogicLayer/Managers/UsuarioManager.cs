
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

        string IUsuarioManager.CreateUsuario(Usuario usuario)
        {
            _Store.SaveUsuario(usuario);
            return "Usuario creado exitosamente";
        }
    }
}
