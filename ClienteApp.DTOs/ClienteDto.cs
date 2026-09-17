using System;
namespace ClienteApp.DTOs
{
	public class ClienteDto
	{
        public string Identificacion { get; set; } = null!;
        public string Nombre_Completo { get; set; } = null!;
        public string? Email { get; set; }
    }
}

