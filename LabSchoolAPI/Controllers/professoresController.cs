using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabSchoolAPI.LabSchool;
using LabSchoolAPI.DTO;
using LabSchoolAPI.Enums;
using LabSchoolAPI.Services.PedagogoService;
using LabSchoolAPI.Services.ProfessorService;

namespace LabSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class professoresController : ControllerBase
    {
        private readonly IProfessoresService _professoresService;

        public professoresController(IProfessoresService professoresService)
        {
            _professoresService = professoresService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessorDTOResposta>>> GetProfessores()
        {
            return await _professoresService.GetProfessores();

        }


        [HttpGet("{codigo}")]
        public async Task<ActionResult<ProfessorDTOResposta>> GetProfessor(int codigo)
        {
            var resultado = await _professoresService.GetProfessor(codigo);

            if (resultado is null)
            {
                return NotFound("Código de professor inexistente.");
            }

            return resultado;
        }


        [HttpPut("{codigo}")]
        public async Task<ActionResult<ProfessorDTOResposta>> PutProfessor(int codigo, ProfessorDTORequisicao professorDTO)
        {
            try
            {
                var resultado = await _professoresService.PutProfessor(codigo, professorDTO);
                if (resultado is null)
                {
                    return NotFound("Professor não encontrado.");
                }
                return resultado;
            }
            catch
            {
                return BadRequest("Operação não realizada.");
            }

        }


        [HttpPost]
        public async Task<ActionResult<ProfessorDTOResposta>> PostProfessor(ProfessorDTORequisicao professorDTOPost)
        {
            try
            {
                var resultado = await _professoresService.PostProfessor(professorDTOPost);
                if (resultado is null)
                {
                    return Conflict("CPF já registrado.");
                }
                return resultado;
            }

            catch
            {
                return BadRequest("Dados inválidos.");
            }
        }


        [HttpDelete("{codigo}")]
        public async Task<ActionResult> DeleteProfessor(int codigo)
        {
            try
            {
                var resultado = await _professoresService.DeleteProfessor(codigo);
                return NoContent();
            }
            catch
            {
                return NotFound("Professor não encontrado.");

            }
        }
    }
}
