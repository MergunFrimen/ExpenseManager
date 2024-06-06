namespace ExpenseManager.Application.Common.Interfaces.Authentication;

public interface IPasswordHasher
{
    bool Verify(string password, string passwordHash);
    string Hash(string password);
}