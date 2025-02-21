using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Login.DoLogin;

public class DoLoginUseCase
{
	public ResponseRegisteredUserJson Execute(RequestLoginJson request)
	{
		var dbContext = new TechLibraryDbContext();

		var user = dbContext.Users.FirstOrDefault(user => user.Email.Equals(request.Email));
		if (user is null)
		{
			throw new InvalidLoginException();
		}

		return new ResponseRegisteredUserJson
		{

		};
	}
}
