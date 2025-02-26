using System.IdentityModel.Tokens.Jwt;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.DataAccess;

namespace TechLibrary.Api.Services.LoggedUser;

public class LoggedUseService
{
	private readonly HttpContext _httpcontext;
	public LoggedUseService(HttpContext httpContext)
	{
		_httpcontext = httpContext;
	}

	public User User(TechLibraryDbContext dbContext)
	{
		var authentication = _httpcontext.Request.Headers.Authorization.ToString();
		
		var token = authentication["Bearer ".Length..].Trim();

		var tookenHandler = new JwtSecurityTokenHandler();
		
		var jwtSecurityToken = tookenHandler.ReadJwtToken(token);

		var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

		var userId = Guid.Parse(identifier);

		return dbContext.Users.First(user => user.Id == userId);
	}
}
