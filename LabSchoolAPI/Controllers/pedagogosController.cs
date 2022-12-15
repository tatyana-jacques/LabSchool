using AutoMapper;
using LabSchoolAPI.DTO;
using LabSchoolAPI.LabSchool;
using LabSchoolAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedagogosController : ControllerBase
    {

        private readonly LabSchoolContext _context;
        private readonly IMapper _mapper;

        public pedagogosController(LabSchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedagogoDTO>>> GetPedagogos()
        {
            var pedagogos = await _context.Pedagogos.ToListAsync();
            List<PedagogoDTO> pedagogosDTO = _mapper.Map<List<PedagogoDTO>>(pedagogos);
            return pedagogosDTO;

        }


        [HttpGet("{codigo}")]
        public async Task<ActionResult<PedagogoDTO>> GetPedagogo(int codigo)
        {
            var pedagogo = await _context.Pedagogos.FindAsync(codigo);
            PedagogoDTO pedagogoDTO = _mapper.Map<PedagogoDTO>(pedagogo);

            if (pedagogo == null)
            {
                return NotFound("Código de pedagogo inexistente.");
            }

            return pedagogoDTO;
        }


        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutPedagogo(PedagogoDTO pedagogoDTO)
        {
            try
            {
                Pedagogo pedagogo = _context.Pedagogos.Where(x => x.Codigo == pedagogoDTO.Codigo).AsNoTracking().FirstOrDefault();
                if (pedagogo == null)
                {
                    return NotFound("Pedagogo não encontrado.");
                }

                pedagogo = _mapper.Map<Pedagogo>(pedagogoDTO);
                _context.Entry(pedagogo).State = EntityState.Modified;
                _context.Pedagogos.Update(pedagogo);
                await _context.SaveChangesAsync();
                return Ok(pedagogoDTO);
            }
            catch
            {
                return BadRequest("Operação não realizada.");
            }

        }

        [HttpPost]
        public async Task<ActionResult<Pedagogo>> PostPedagogo(PedagogoDTOPost pedagogoDTOPost)
        {
            try
            {
                Pedagogo pedagogo = _mapper.Map<Pedagogo>(pedagogoDTOPost);
                var pedagogos = await _context.Alunos.ToListAsync();

                var resultado = pedagogos.Where(x => x.CPF == pedagogoDTOPost.CPF).FirstOrDefault();
                if (resultado is not null)
                {
                    return Conflict("CPF já registrado.");
                }

                _context.Entry(pedagogo).State = EntityState.Added;
                await _context.SaveChangesAsync();

                var pedagogoDTO = _mapper.Map<PedagogoDTO>(pedagogo);

                return CreatedAtAction("GetPedagogo", new { codigo = pedagogo.Codigo }, pedagogoDTO);
            }

            catch
            {
                return BadRequest("Dados inválidos.");
            }
        }



        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeletePedagogo(int codigo)
        {
            try
            {
                var pedagogo = await _context.Pedagogos.FindAsync(codigo);
                _context.Pedagogos.Remove(pedagogo);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return NotFound("Pedagogo não encontrado.");

            }
        }

        private bool ProfessorExists(int id)
        {
            return _context.Pedagogos.Any(e => e.Codigo == id);
        }
    }
}

