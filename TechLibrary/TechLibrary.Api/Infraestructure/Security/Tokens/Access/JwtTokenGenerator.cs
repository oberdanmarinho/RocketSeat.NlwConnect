using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infraestructure.Security.Tokens.Access;

public class JwtTokenGenerator
{
	public string Generate(User user)
	{
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Expires = DateTime.UtcNow.AddMinutes(60),
			SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
		};

		var tokenHandler = new JwtSecurityTokenHandler();

		var securityToken = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(securityToken);
	}

	private static SymmetricSecurityKey SecurityKey()
	{
		var signingKey = "d7lliXY4SXxv4zFh762G8WWLrsOmpQnf";

		var symmetricKey = Encoding.UTF8.GetBytes(signingKey);

		return new SymmetricSecurityKey(symmetricKey);
	}
}
