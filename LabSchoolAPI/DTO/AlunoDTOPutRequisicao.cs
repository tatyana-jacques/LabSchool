using System.Security.Policy;
using LabSchoolAPI.Models;
using LabSchoolAPI.Enums;

namespace LabSchoolAPI.DTO
{
    public class AlunoDTOPutRequisicao
    {

        public string Situacao { get; set; }

        public static explicit operator AlunoDTOPutRequisicao(Aluno aluno)
        {
            return new AlunoDTOPutRequisicao
            {
                Situacao = aluno.Situacao.ToString(),

            };
        }

    }
}
