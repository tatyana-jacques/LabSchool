using LabSchoolAPI.Enums;
using LabSchoolAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace LabSchoolAPI.DTO
{
    public class AlunoDTO: PessoaDTO
    {
        public string SituacaoMatricula { get; set; }
        public float Nota { get; set; }
        public int QtdAtendimentos { get; set; }
    }
}
