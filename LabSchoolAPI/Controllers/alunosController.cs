using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabSchoolAPI.LabSchool;
using LabSchoolAPI.Models;
using AutoMapper;
using LabSchoolAPI.DTO;
using LabSchoolAPI.Services;
using System.Data;
using LabSchoolAPI.Enums;
using LabSchoolAPI.Services.AlunoService;

namespace LabSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class alunosController : ControllerBase
    {
        private readonly IAlunosService _alunosService;
        private readonly IMapper _mapper;

        public alunosController(IAlunosService alunosService, IMapper mapper)
        {
            _alunosService = alunosService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTOResposta>>> GetAlunos(string? situacao)
        {
            var resultado = await _alunosService.GetAlunos(situacao);

            if (resultado is null)
            {
                return NotFound("Situação inexistente.");
            }

            return resultado;

        }


        [HttpGet("{codigo}")]
        public async Task<ActionResult<AlunoDTOResposta>> GetAluno(int codigo)
        {
            var resultado = await _alunosService.GetAluno(codigo);

            if (resultado is null)
            {
                return NotFound("Código de aluno inexistente.");
            }

            return resultado;
        }


        [HttpPut("{codigo}")]
        public async Task<ActionResult<AlunoDTOResposta>> PutAluno(int codigo, AlunoDTOPutRequisicao alunoDTOPut)
        {
            try
            {
                var resultado = await _alunosService.PutAluno(codigo, alunoDTOPut);
                if (resultado is null)
                {
                    return NotFound("Aluno não encontrado.");
                }
                return resultado;
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
                var validacaoAluno = AlunoPostValidacao.ValidacaoALuno(alunoDTOPost);

                if (validacaoAluno != string.Empty)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, validacaoAluno.ToString());
                }

                Aluno resultado = await _alunosService.PostAluno(alunoDTOPost);
                if (resultado is null)
                {
                    return Conflict("CPF já registrado.");
                }

                var alunoDTO = _mapper.Map<AlunoDTOResposta>(resultado);

                return CreatedAtAction("GetAluno", new { codigo = resultado.Codigo }, alunoDTO);
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
                var resultado = await _alunosService.DeleteAluno(codigo);
                return NoContent();
            }
            catch
            {
                return NotFound("Aluno não encontrado.");

            }

        }

        
    }
}
