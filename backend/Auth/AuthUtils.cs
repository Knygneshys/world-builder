using System.Security.Claims;
using backend.Data.Entities.Enums;

namespace backend.Auth;

public static class AuthUtils
{
    public static bool IsAdmin(ClaimsPrincipal user) => user.IsInRole(nameof(Roles.Admin));
}
