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
        public async Task<ActionResult<IEnumerable<PedagogoDTOResposta>>> GetPedagogos()
        {
            var pedagogos = await _context.Pedagogos.ToListAsync();
            List<PedagogoDTOResposta> pedagogosDTO = _mapper.Map<List<PedagogoDTOResposta>>(pedagogos);
            return pedagogosDTO;

        }


        [HttpGet("{codigo}")]
        public async Task<ActionResult<PedagogoDTOResposta>> GetPedagogo(int codigo)
        {
            var pedagogo = await _context.Pedagogos.FindAsync(codigo);
            PedagogoDTOResposta pedagogoDTO = _mapper.Map<PedagogoDTOResposta>(pedagogo);

            if (pedagogo == null)
            {
                return NotFound("Código de pedagogo inexistente.");
            }

            return pedagogoDTO;
        }


        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutPedagogo(int codigo, PedagogoDTORequisicao pedagogoDTO)
        {
            try
            {
                Pedagogo pedagogo = await _context.Pedagogos.FindAsync(codigo);
                if (pedagogo == null)
                {
                    return NotFound("Pedagogo não encontrado.");
                }

                pedagogo.Nome= pedagogoDTO.Nome;
                pedagogo.CPF=pedagogoDTO.CPF;
                pedagogoDTO.DataNascimento = pedagogoDTO.DataNascimento;
                _context.Entry(pedagogo).State = EntityState.Modified;
                _context.Pedagogos.Update(pedagogo);
                await _context.SaveChangesAsync();

                PedagogoDTOResposta pedagogoDTOResposta = _mapper.Map<PedagogoDTOResposta>(pedagogo);
                return Ok(pedagogoDTOResposta);
            }
            catch
            {
                return BadRequest("Operação não realizada.");
            }

        }

        [HttpPost]
        public async Task<ActionResult<PedagogoDTOResposta>> PostPedagogo(PedagogoDTORequisicao pedagogoDTOPost)
        {
            try
            {
                Pedagogo pedagogo = _mapper.Map<Pedagogo>(pedagogoDTOPost);
                var pedagogos = await _context.Pedagogos.ToListAsync();

                var resultado = pedagogos.Where(x => x.CPF == pedagogoDTOPost.CPF).FirstOrDefault();
                if (resultado is not null)
                {
                    return Conflict("CPF já registrado.");
                }

                _context.Entry(pedagogo).State = EntityState.Added;
                await _context.SaveChangesAsync();

                var pedagogoDTO = _mapper.Map<PedagogoDTOResposta>(pedagogo);

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

