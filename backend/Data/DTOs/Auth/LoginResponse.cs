namespace backend.Data.DTOs.Auth;

public record LoginResponse(string AccessToken, string RefreshToken);