using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace ClienteApp.Repositorie
{

    public interface IClienteRepository
    {
        Task<Cliente?> ObtenerPorIdentificacionAsync(string identificacion);
    }

    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
		{
            _context = context;
        }

        public async Task<Cliente?> ObtenerPorIdentificacionAsync(string identificacion)
        {
            var param = new SqlParameter("@Identificacion", identificacion);

            var clientes = await _context.Clientes
                .FromSqlRaw("EXEC sp_ObtenerClientePorIdentificacion @Identificacion", param)
                .ToListAsync();

            return clientes.FirstOrDefault();
        }
    }
}

