using System.ComponentModel.DataAnnotations;

namespace Tema.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public string Autor { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataCompletarii { get; set; }
        public int IdJoc { get; set; }
        
    }
}
