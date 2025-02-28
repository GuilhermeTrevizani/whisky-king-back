using WhiskyKing.Core.Interfaces.Services;
using BC = BCrypt.Net.BCrypt;

namespace WhiskyKing.Core.Services;

public class HashService : IHashService
{
    public string Hash(string text) => BC.HashPassword(text);

    public bool Verify(string text, string hash) => BC.Verify(text, hash);
}