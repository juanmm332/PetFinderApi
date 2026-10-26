using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Security
{
    public abstract class AuthorizedApiController : ControllerBase
    {
        protected int? CurrentUserId =>
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
                ? userId
                : null;

        protected bool IsAdministrator => User.IsInRole("Admin");
    }
}
