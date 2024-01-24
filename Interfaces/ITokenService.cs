using finshark.Models;

namespace finshark.Interfaces;

public interface ITokenService
{
    public string CreateToken(User user);
}