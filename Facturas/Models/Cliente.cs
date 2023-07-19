
using System.ComponentModel.DataAnnotations;

namespace Facturas.Models
{
    public class Cliente
    {

        public int ID { get; set; }
        public string Nombre { get; set; }
        public string NIF { get; set; }
        public string Domicilio { get; set; }
        public string Poblacion { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public DateTime FechaAlta { get; set; }

    }
}
