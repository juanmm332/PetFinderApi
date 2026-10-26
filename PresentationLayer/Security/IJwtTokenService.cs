using EntitiesLayer;

namespace PresentationLayer.Security
{
    public interface IJwtTokenService
    {
        string CreateToken(Usuario usuario);
    }
}
