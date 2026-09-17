using System;
using ClienteApp.DTOs;

namespace ClienteApp.Services
{
	public interface IClienteService
	{
        Task<ClienteDto?> ObtenerClienteAsync(string identificacion);
    }
}

