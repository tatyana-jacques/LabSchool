using System.ComponentModel.DataAnnotations;
using LabSchoolAPI.Enums;

namespace LabSchoolAPI.Models
{
    public class Aluno: Pessoa
    {
        public EnumSituacaoMatricula SituacaoMatricula { get; set; }

        [Range (0,10)]
        public float Nota { get; set; }
        public int QtdAtendimentos { get; set; }

        
    }
}
