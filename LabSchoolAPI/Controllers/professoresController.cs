using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabSchoolAPI.LabSchool;
using LabSchoolAPI.Models;
using AutoMapper;
using LabSchoolAPI.DTO;
using LabSchoolAPI.Enums;

namespace LabSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class professoresController : ControllerBase
    {
        private readonly LabSchoolContext _context;
        private readonly IMapper _mapper;

        public professoresController(LabSchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessorDTOResposta>>> GetProfessores()
        {
            var professores = await _context.Professores.ToListAsync();
            List<ProfessorDTOResposta> professoresDTO = _mapper.Map<List<ProfessorDTOResposta>>(professores);
            return professoresDTO;

        }


        [HttpGet("{codigo}")]
        public async Task<ActionResult<ProfessorDTOResposta>> GetProfessor(int codigo)
        {
            var professor = await _context.Professores.FindAsync(codigo);
            ProfessorDTOResposta professorDTO = _mapper.Map<ProfessorDTOResposta>(professor);

            if (professor == null)
            {
                return NotFound("Código de professor inexistente.");
            }

            return professorDTO;
        }

       
        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutProfessor(int codigo, ProfessorDTORequisicao professorDTO)
        {
            try
            {
                Professor professor = await _context.Professores.FindAsync(codigo);
                if (professor == null)
                {
                    return NotFound("Professor não encontrado.");
                }
                professor.Nome = professorDTO.Nome;
                professor.Telefone=professorDTO.Telefone;
                professor.DataNascimento= professorDTO.DataNascimento;
                professor.Formacao = (EnumFormacaoAcademica)Enum.Parse(typeof(EnumFormacaoAcademica), professorDTO.Formacao.ToUpper());
                _context.Entry(professor).State = EntityState.Modified;
                _context.Professores.Update(professor);
                await _context.SaveChangesAsync();

                ProfessorDTOResposta professorDTOResposta = _mapper.Map<ProfessorDTOResposta>(professor);
                return Ok(professorDTO);
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
                    Professor professor = _mapper.Map<Professor>(professorDTOPost);
                    var professores = await _context.Professores.ToListAsync();

                    var resultado = professores.Where(x => x.CPF == professorDTOPost.CPF).FirstOrDefault();
                    if (resultado is not null)
                    {
                        return Conflict("CPF já registrado.");
                    }

                    _context.Entry(professor).State = EntityState.Added;
                    await _context.SaveChangesAsync();

                    var professorDTO = _mapper.Map<ProfessorDTOResposta>(professor);

                    return CreatedAtAction("GetProfessor", new { codigo = professor.Codigo }, professorDTO);
                }

                catch
                {
                    return BadRequest("Dados inválidos.");
                }
            }
        

        
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteProfessor(int codigo)
        {
            try
            {
                var professor = await _context.Professores.FindAsync(codigo);
                _context.Professores.Remove(professor);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return NotFound("Professor não encontrado.");

            }
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professores.Any(e => e.Codigo == id);
        }
    }
}
