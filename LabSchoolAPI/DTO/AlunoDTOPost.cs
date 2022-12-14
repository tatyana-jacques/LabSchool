using System.ComponentModel.DataAnnotations;


namespace LabSchoolAPI.DTO
{
    public class AlunoDTOPost
    {
        [Required, StringLength(150)]
        public string Nome { get; set; }

        [Required, StringLength(13)]
        public string Telefone { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public long CPF { get; set; }

        [Required]
        public string Situacao { get; set; }
       
        [Required]
        public float Nota { get; set; }
    }
}
