using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabSchoolAPI.LabSchool;
using LabSchoolAPI.Models;
using AutoMapper;
using LabSchoolAPI.DTO;
using LabSchoolAPI.Services;
using System.Data;
using LabSchoolAPI.Enums;

namespace LabSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class alunosController : ControllerBase
    {
        private readonly LabSchoolContext _context;
        private readonly IMapper _mapper;

        public alunosController(LabSchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTOResposta>>> GetAlunos(string? situacao)
        {
            var alunos = await _context.Alunos.ToListAsync();

            if (situacao is not null)
            {
                var alunosQuery = alunos.Where(x => x.Situacao.ToString() == situacao.ToUpper());

                if (alunosQuery.Count() == 0)
                {
                    return NotFound("Situação inexistente.");

                }

                List<AlunoDTOResposta> alunosDTO = _mapper.Map<List<AlunoDTOResposta>>(alunosQuery);
                return alunosDTO;
            }

            else
            {
                List<AlunoDTOResposta> alunosDTO = _mapper.Map<List<AlunoDTOResposta>>(alunos);
                return alunosDTO;
            }
        }


        [HttpGet("{codigo}")]
        public async Task<ActionResult<AlunoDTOResposta>> GetAluno(int codigo)
        {
            var aluno = await _context.Alunos.FindAsync(codigo);
            AlunoDTOResposta alunoDTO = _mapper.Map<AlunoDTOResposta>(aluno);

            if (aluno == null)
            {
                return NotFound("Código de aluno inexistente.");
            }

            return alunoDTO;
        }


        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutAluno(int codigo, AlunoDTOPutRequisicao alunoDTOPut)
        {
            try
            {
                Aluno aluno = await _context.Alunos.FindAsync(codigo);
                if (aluno == null)
                {
                    return NotFound("Aluno não encontrado.");
                }
                aluno.Situacao = (EnumSituacaoMatricula)Enum.Parse(typeof(EnumSituacaoMatricula), alunoDTOPut.Situacao.ToUpper());
                _context.Entry(aluno).State = EntityState.Modified;
                _context.Alunos.Update(aluno);
                await _context.SaveChangesAsync();

                AlunoDTOResposta alunoDTO = _mapper.Map<AlunoDTOResposta>(aluno);
                return Ok(alunoDTO);
            }
            catch
            {
                return BadRequest("Situação inválida.");
            }

        }


        [HttpPost]
        public async Task<ActionResult<AlunoDTOResposta>> PostAluno(AlunoDTOPostRequisicao alunoDTOPost)
        {
            try
            {
                Aluno aluno = _mapper.Map<Aluno>(alunoDTOPost);
                var alunos = await _context.Alunos.ToListAsync();
                var validacaoAluno = AlunoPostValidacao.ValidacaoALuno(aluno);

                if (validacaoAluno != string.Empty)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, validacaoAluno.ToString());
                }

                var resultado = alunos.Where(x => x.CPF == alunoDTOPost.CPF).FirstOrDefault();
                if (resultado is not null)
                {
                    return Conflict("CPF já registrado.");
                }

                _context.Entry(aluno).State = EntityState.Added;
                await _context.SaveChangesAsync();

                var alunoDTO = _mapper.Map<AlunoDTOResposta>(aluno);

                return CreatedAtAction("GetAluno", new { codigo = aluno.Codigo }, alunoDTO);
            }

            catch
            {
                return BadRequest("Dados inválidos.");
            }
        }


        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteAluno(int codigo)
        {
            try
            {
                var aluno = await _context.Alunos.FindAsync(codigo);
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return NotFound();

            }

        }

        private bool AlunoExists(int codigo)
        {
            return _context.Alunos.Any(e => e.Codigo == codigo);
        }
    }
}
