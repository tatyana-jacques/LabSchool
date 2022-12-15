using LabSchoolAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LabSchoolAPI.Models
{
    public class Professor : Pessoa
    {
        [Required]
        public EnumFormacaoAcademica Formacao { get; set; }

        [Required]
        public EnumExperiencia Experiencia { get; set; }
        [Required]
        public EnumEstado Estado { get; set; }
      
        
    }
}
