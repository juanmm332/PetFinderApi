using EntitiesLayer;

namespace BusinessLogicLayer
{
    public interface IUsuarioManager
    {
        List<Usuario> GetAllUsuarios();
        string CreateUsuario(Usuario usuario);
    }
}