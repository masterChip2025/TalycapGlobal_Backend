using System;
using Microsoft.EntityFrameworkCore;
namespace ClienteApp.Repositorie
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options){}
        public DbSet<Cliente> Clientes { get; set; }
    }
}

