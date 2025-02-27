using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Api.Services.LoggedUser;
using TechLibrary.Api.UseCases.Checkouts;

namespace TechLibrary.Api.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize]
public class CheckoutsController : ControllerBase
{
	private readonly TechLibraryDbContext _dbContext;
	private readonly LoggedUseService _loggedUser;

	public CheckoutsController(TechLibraryDbContext dbContext, IHttpContextAccessor httpContextAccessor)
	{
		_dbContext = dbContext;
		_loggedUser = new LoggedUseService(httpContextAccessor.HttpContext, _dbContext);
	}

	[HttpPost]
	[Route("{bookId}")]
	public IActionResult BookCheckout(Guid bookId)
	{
		var useCase = new RegisterBookCheckoutsUseCase(_loggedUser, _dbContext);
		
		useCase.Execute(bookId);

		return NoContent();
	}
}
