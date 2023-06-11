using System.ComponentModel.DataAnnotations;

namespace Tema.Models
{
    public class Joc
    {
        public int Id { get; set; }
        public string? Titlu { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataLansarii { get; set; }
        public string? Gen { get; set; }
        public decimal Pret { get; set; }
    }
}
