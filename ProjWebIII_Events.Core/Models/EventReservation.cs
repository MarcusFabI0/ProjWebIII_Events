using System.ComponentModel.DataAnnotations;

namespace ProjWebIII_Events.Core.Models
{
    public class EventReservation
    {
        public long IdReservation { get; set; }

        [Required(ErrorMessage = "ID é obrigatório")]
        public long IdEvent { get; set; }

        [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória")]
        public long Quantity { get; set; }
    }
}

//EventReservation:
//IdReservation        long incremento PK
//IdEvent                long        not null FK
//PersonName            string        not null
//Quantity            long        not null
