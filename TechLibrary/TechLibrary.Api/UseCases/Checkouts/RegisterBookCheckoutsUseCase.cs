using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Api.Services.LoggedUser;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Checkouts;

public class RegisterBookCheckoutsUseCase
{
	private const int MAX_LOAN_DAYS = 7;

	private readonly LoggedUseService _loggedUse;

	public RegisterBookCheckoutsUseCase(LoggedUseService loggedUse)
	{
		_loggedUse = loggedUse;
	}

	public void Execute(Guid bookId)
	{
		var dbContext = new TechLibraryDbContext();

		Validate(dbContext, bookId);

		var user = _loggedUse.User();

		var entity = new Domain.Entities.Checkout
		{
			UserId = user.Id,
			BookId = bookId,
			ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS)
		};

		dbContext.Checkouts.Add(entity);

		dbContext.SaveChanges();
	}

	private void Validate(TechLibraryDbContext dbContext, Guid bookId)
	{
		var book = dbContext.Books.FirstOrDefault(book => book.Id == bookId);
		if (book is null)
			throw new NotFoundException("Livro não encontrado.");

		var amoutBooksNotRetorned = dbContext
			.Checkouts
			.Count(checkout => checkout.BookId == bookId && checkout.ReturnDate == null);

		if (amoutBooksNotRetorned == book.Amount)
			throw new ConflictException("O livro não está dispnível para empréstimo.");
	}
}
