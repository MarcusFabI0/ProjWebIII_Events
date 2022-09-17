using System.ComponentModel.DataAnnotations;

namespace ProjWebIII_Events.Core.Models
{
    public class CityEvent
    {
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Título é obrigatório")]
        [MaxLength(100, ErrorMessage = "Titulo deve ter no máximo 100 caracteres")]
        public string Title { get; set; }

        [MaxLength(100, ErrorMessage = "Descrição deve ter no máximo 1000 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        public DateTime DateHourEvent { get; set; }

        [Required(ErrorMessage = "Local é obrigatório")]
        [MaxLength(100, ErrorMessage = "Local deve ter no máximo 100 caracteres")]
        public string Local { get; set; }

        [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Address { get; set; }

       
        public decimal Price { get; set; }

        public bool Status {get; set;}
        

            

    }
}


//        IdEvent long incremento PK
//Title                 string not null
//Description string        null
//DateHourEvent DateTime    not null
//Local string not null
//Address string        null
//Price decimal        null
//Status bit        not null