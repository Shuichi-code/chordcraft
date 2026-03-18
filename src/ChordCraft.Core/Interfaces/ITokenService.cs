using ChordCraft.Core.Entities;

namespace ChordCraft.Core.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
