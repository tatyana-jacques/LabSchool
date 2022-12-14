using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace LabSchoolAPI.DTO
{
    public class PessoaDTO
    {
      
        public int Codigo { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string DataNascimento { get; set; }

        public long CPF { get; set; }


    }
}
