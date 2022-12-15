using System.Security.Policy;
using LabSchoolAPI.Models;
using LabSchoolAPI.Enums;

namespace LabSchoolAPI.DTO
{
    public class AlunoDTOPut
    {

        public string Situacao { get; set; }  

        //public static implicit operator AlunoDTOPut(Aluno aluno)
        //{
        //    return new AlunoDTOPut
        //    {
        //        Situacao = aluno.Situacao.ToString(),

        //    };
        //}

    }
}
