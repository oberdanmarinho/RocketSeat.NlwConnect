using Microsoft.EntityFrameworkCore;
using TechLibrary.Api.Controllers;
using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infraestructure.DataAccess;

public class TechLibraryDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Book> Books { get; set; }

	public DbSet<Checkout> Checkouts { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source=D:\\Studies\\Rocketseat\\Rocketseat.NlwConnect\\DataBase\\TechLibraryDb.db");
	}
}
