using System.Threading.Tasks;
using IdentityModel.Client;

namespace Contacts.WebClient.Services
{
  public interface ITokenService
  {
    Task<TokenResponse> GetToken(string scope);
  }
}