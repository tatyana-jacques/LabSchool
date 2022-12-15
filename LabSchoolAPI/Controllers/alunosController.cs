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
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetAlunos(string? situacao)
        {
            var alunos = await _context.Alunos.ToListAsync();

            if (situacao is not null)
            {  
                var alunosQuery = alunos.Where(x => x.Situacao.ToString() == situacao.ToUpper());
                
                if (alunosQuery.Count() >= 1)
                {
                List<AlunoDTO> alunosDTO = _mapper.Map<List<AlunoDTO>>(alunosQuery);
                return alunosDTO;
                }
                
                else
                {
                    return NotFound("Situação inexistente.");
                }         
            }

            else
            {
            List<AlunoDTO> alunosDTO = _mapper.Map<List<AlunoDTO>>(alunos);
            return alunosDTO;
            }
        }

        
        [HttpGet("{codigo}")]
        public async Task<ActionResult<AlunoDTO>> GetAluno(int codigo)
        {
            var aluno = await _context.Alunos.FindAsync(codigo);
            AlunoDTO alunoDTO = _mapper.Map<AlunoDTO>(aluno);

            if (aluno == null)
            {
                return NotFound("Código de aluno inexistente.");
            }

            return alunoDTO;
        }

        
        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutAluno(int codigo, AlunoDTOPut alunoDTOPut)
        {
            try
            {
            Aluno aluno = await _context.Alunos.FindAsync(codigo);
                if (aluno == null)
                {
                    return NotFound("Aluno não encontrado.");
                }
            aluno.Situacao = (EnumSituacaoMatricula) Enum.Parse(typeof(EnumSituacaoMatricula), alunoDTOPut.Situacao.ToUpper());
            _context.Entry(aluno).State = EntityState.Modified;
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
            
                AlunoDTO alunoDTO = _mapper.Map<AlunoDTO>(aluno);
                return Ok(alunoDTO);
            }
            catch
            {
                return BadRequest("Situação inválida.");
            }
           
        }

        
        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(AlunoDTOPost alunoDTOPost)
        {
            try
            {
                Aluno aluno = _mapper.Map<Aluno>(alunoDTOPost);
                var alunos = await _context.Alunos.ToListAsync();
                var validacaoAluno = AlunoPostValidacao.ValidacaoALuno(aluno);

                if (validacaoAluno != string.Empty)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable,validacaoAluno.ToString());
                }

                var resultado = alunos.Where(x => x.CPF == alunoDTOPost.CPF).FirstOrDefault();
                if (resultado is not null)
                {
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
