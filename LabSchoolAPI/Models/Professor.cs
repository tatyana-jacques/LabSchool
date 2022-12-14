using LabSchoolAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LabSchoolAPI.Models
{
    public class Professor : Pessoa
    {
        public EnumFormacaoAcademica Formacao { get; set; }
        public EnumExperiencia Experiencia { get; set; }
        public EnumEstado Estado { get; set; }
      
        
    }
}
