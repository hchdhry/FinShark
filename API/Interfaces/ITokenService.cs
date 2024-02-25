using API.Models;

namespace API;

public interface ITokenService
{
    public string CreateToken(AppUser User);

}
