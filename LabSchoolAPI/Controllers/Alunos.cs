using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabSchoolAPI.LabSchool;
using LabSchoolAPI.Models;
using AutoMapper;
using LabSchoolAPI.DTO;
using LabSchoolAPI.Services;

namespace LabSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Alunos : ControllerBase
    {
        private readonly LabSchoolContext _context;
        private readonly IMapper _mapper;

        public Alunos(LabSchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Alunos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetAlunos()
        {
            var alunos = await _context.Alunos.ToListAsync();
            List<AlunoDTO> alunosDTO = _mapper.Map<List<AlunoDTO>>(alunos);

            return alunosDTO;
        }

        // GET: api/Alunos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

          


            return aluno;
        }

        // PUT: api/Alunos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {
            if (id != aluno.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Alunos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(AlunoDTOPost alunoDTOPost)
        {

            try
            {
              
            Aluno aluno = _mapper.Map<Aluno>(alunoDTOPost);
            aluno.Atendimentos = 0;

            var alunos = await _context.Alunos.ToListAsync();
            var validacaoAluno = AlunoPostValidacao.ValidacaoALuno(aluno);

                if (validacaoAluno != string.Empty)
                {
                    return BadRequest(validacaoAluno.ToString());
                }

                foreach (Aluno x in alunos)
                {
                    if (x.CPF == aluno.CPF)
                        return Conflict("CPF já registrado.");

                }


            _context.Entry(aluno).State = EntityState.Added;
            await _context.SaveChangesAsync();

            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

            return CreatedAtAction("GetAluno", new { id = aluno.Codigo }, alunoDTO);
            }

            catch
            {
                return BadRequest("Dados inválidos.");
            }
        }


        // DELETE: api/Alunos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.Codigo == id);
        }
    }
}
