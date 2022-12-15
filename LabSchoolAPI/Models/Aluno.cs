using System.ComponentModel.DataAnnotations;
using LabSchoolAPI.Enums;

namespace LabSchoolAPI.Models
{
    public class Aluno: Pessoa
    {
        public EnumSituacaoMatricula Situacao { get; set; }

        [Required, Range (0,10)]
        public float Nota { get; set; }
        public int Atendimentos { get; set; } = 0;

        
    }
}
