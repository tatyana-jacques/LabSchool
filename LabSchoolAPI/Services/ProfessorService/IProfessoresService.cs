using LabSchoolAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LabSchoolAPI.Services.ProfessorService
{
    public interface IProfessoresService
    {
        Task<ActionResult<IEnumerable<ProfessorDTOResposta>>> GetProfessores();
        Task<ActionResult<ProfessorDTOResposta>> GetProfessor(int codigo);
        Task<ActionResult<ProfessorDTOResposta>> PutProfessor(int codigo, ProfessorDTORequisicao professorDTO);
        Task<ActionResult<ProfessorDTOResposta>> PostProfessor(ProfessorDTORequisicao professorDTOPost);
        Task<ActionResult> DeleteProfessor(int codigo);
    }
}
