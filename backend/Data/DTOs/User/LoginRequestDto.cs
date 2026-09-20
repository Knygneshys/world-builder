namespace backend.Data.DTOs.User;

public record LoginRequestDto(
    string Email,
    string Password
);