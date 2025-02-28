namespace WhiskyKing.Core.Interfaces.Services;

public interface IHashService
{
    string Hash(string text);
    bool Verify(string text, string hash);
}