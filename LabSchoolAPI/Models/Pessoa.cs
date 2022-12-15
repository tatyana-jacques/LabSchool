using System.ComponentModel.DataAnnotations;

namespace LabSchoolAPI.Models
{
    public abstract class Pessoa
    {
        [Key]
        public int Codigo { get; set; }
        
        [Required, StringLength(150)]
        public string Nome { get; set; }
        
        [Required, StringLength(13)]
        public string Telefone { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
        
        [Required]
        public long CPF { get; set; }

      
    }
}
