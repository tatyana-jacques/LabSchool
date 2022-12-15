using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabSchoolAPI.LabSchool;
using LabSchoolAPI.Models;
using AutoMapper;
using LabSchoolAPI.DTO;


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
        public async Task<ActionResult<IEnumerable<ProfessorDTO>>> GetProfessores()
        {
            var professores = await _context.Professores.ToListAsync();
            List<ProfessorDTO> professoresDTO = _mapper.Map<List<ProfessorDTO>>(professores);
            return professoresDTO;

        }


        [HttpGet("{codigo}")]
        public async Task<ActionResult<ProfessorDTO>> GetProfessor(int codigo)
        {
            var professor = await _context.Professores.FindAsync(codigo);
            ProfessorDTO professorDTO = _mapper.Map<ProfessorDTO>(professor);

            if (professor == null)
            {
                return NotFound("Código de professor inexistente.");
            }

            return professorDTO;
        }

       
        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutProfessor(ProfessorDTO professorDTO)
        {
            try
            {
               Professor  professor = _context.Professores.Where(x => x.Codigo == professorDTO.Codigo).AsNoTracking().FirstOrDefault();
                if (professor == null)
                {
                    return NotFound("Professor não encontrado.");
                }

                professor = _mapper.Map<Professor>(professorDTO);
                _context.Entry(professor).State = EntityState.Modified;
                _context.Professores.Update(professor);
                await _context.SaveChangesAsync();
                return Ok(professorDTO);
            }
            catch
            {
                return BadRequest("Operação não realizada.");
            }
         
        }

        [HttpPost]
            public async Task<ActionResult<Professor>> PostProfessor(ProfessorDTOPost professorDTOPost)
            {
                try
                {
                    Professor professor = _mapper.Map<Professor>(professorDTOPost);
                    var professores = await _context.Alunos.ToListAsync();

                    var resultado = professores.Where(x => x.CPF == professorDTOPost.CPF).FirstOrDefault();
                    if (resultado is not null)
                    {
                        return Conflict("CPF já registrado.");
                    }

                    _context.Entry(professor).State = EntityState.Added;
                    await _context.SaveChangesAsync();

                    var professorDTO = _mapper.Map<ProfessorDTO>(professor);

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
