using System;
using System.ComponentModel.DataAnnotations;

namespace ClienteApp.Repositorie
{
	public class Cliente
	{
        [Key]
        public int Id { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

