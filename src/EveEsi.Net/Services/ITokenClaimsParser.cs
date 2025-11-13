using System.Security.Claims;

namespace EveEsi.Net.Services;

public interface ITokenClaimsParser
{
	ClaimsPrincipal ParseToken(string token);
}
