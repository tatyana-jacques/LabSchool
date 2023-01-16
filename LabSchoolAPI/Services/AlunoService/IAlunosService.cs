using LabSchoolAPI.DTO;
using LabSchoolAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabSchoolAPI.Services.AlunoService
{
    public interface IAlunosService
    {
        Task<ActionResult<IEnumerable<AlunoDTOResposta>>> GetAlunos(string? situacao);
        Task<ActionResult<AlunoDTOResposta>> GetAluno(int codigo);
        Task<ActionResult<AlunoDTOResposta>> PutAluno(int codigo, AlunoDTOPutRequisicao alunoDTOPut);
        Task <Aluno> PostAluno(AlunoDTOPostRequisicao alunoDTOPost);
        Task<ActionResult> DeleteAluno(int codigo);
    }
}
