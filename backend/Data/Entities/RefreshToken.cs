using Microsoft.AspNetCore.Identity;

namespace backend.Data.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public required string Token { get; set; }
    public required string UserId { get; set; }
    public DateTime ExpiresOnUtc { get; set; }
    public IdentityUser User { get; set; } = null!;
}
