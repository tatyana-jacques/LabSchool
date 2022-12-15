using LabSchoolAPI.Enums;

namespace LabSchoolAPI.DTO
{
    public class ProfessorDTO: PessoaDTO
    {

        public string Formacao { get; set; }
        public string Experiencia { get; set; }
        public string Estado { get; set; }

    }
}
