using System.ComponentModel.DataAnnotations;

namespace LabSchoolAPI.Models
{
    public abstract class Pessoa
    {
        [Key]
        public int Codigo { get; set; }
        
        [StringLength(150)]
        public string Nome { get; set; }
        
        [StringLength(13)]
        public string Telefone { get; set; }

        public DateTime DataNascimento { get; set; }

        public long CPF { get; set; }

      
    }
}
