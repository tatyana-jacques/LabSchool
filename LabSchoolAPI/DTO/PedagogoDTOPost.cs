namespace LabSchoolAPI.DTO
{
    public class PedagogoDTOPost
    {
        public string Nome { get; set; }

        public string Telefone { get; set; }

        public DateTime DataNascimento { get; set; }

        public long CPF { get; set; }

        public float Nota { get; set; }

        public int Atendimentos { get; set; }
    }
}
