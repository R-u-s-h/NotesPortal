using Microsoft.AspNetCore.Identity;
using NotesApp.DbStuff.Models.Notes;
using NotesApp.Services.AutoRegistrationInDI;

namespace NotesApp.Services;

[AutoResolve]
public class PasswordService
{
    private readonly PasswordHasher<User> _hasher = new();

    public string HashPassword(User user, string password)
    {
        return _hasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string password, string passwordHash)
    {
        var result = _hasher.VerifyHashedPassword(user, passwordHash, password);
        return result == PasswordVerificationResult.Success;
    }
}