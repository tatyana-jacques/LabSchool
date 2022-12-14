using LabSchoolAPI.Enums;
using LabSchoolAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace LabSchoolAPI.DTO
{
    public class AlunoDTO: PessoaDTO
    {
        public string Situacao { get; set; }
        public float Nota { get; set; }
        public int Atendimentos { get; set; }
    }
}
