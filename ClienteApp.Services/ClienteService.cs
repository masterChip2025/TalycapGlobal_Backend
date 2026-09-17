using System;
using ClienteApp.DTOs;
using ClienteApp.Repositorie;

namespace ClienteApp.Services
{
	public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        public ClienteService(IClienteRepository repository)
		{
            _repository = repository;
        }

        public async Task<ClienteDto?> ObtenerClienteAsync(string identificacion)
        {
            var cliente = await _repository.ObtenerPorIdentificacionAsync(identificacion);
            if (cliente == null) return null;

            return new ClienteDto
            {
                Identificacion = cliente.Identificacion,
                Nombre_Completo = $"{cliente.Nombre} {cliente.Apellido}",
                Email = cliente.Email
            };
        }
    }
}

