using System.ComponentModel.DataAnnotations;


namespace LabSchoolAPI.DTO
{
    public class AlunoDTOPostRequisicao
    {
        
        public string Nome { get; set; }

        public string Telefone { get; set; }

        public DateTime DataNascimento { get; set; }

        public long CPF { get; set; }
      
        public string Situacao { get; set; }
       
        public float Nota { get; set; }
    }
}
