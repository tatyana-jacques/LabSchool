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

    public class atendimentosController : ControllerBase
    {
       
        private readonly LabSchoolContext _context;
        private readonly IMapper _mapper;

        public atendimentosController(LabSchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPut]
        public async Task<IActionResult> PutAtendimento(AtendimentoDTORequisicao atendimento)
        {
            try
            {
                
                Aluno alunoModel = await _context.Alunos.FindAsync(atendimento.codigoAluno);
                if (alunoModel == null)
                {
                    return NotFound("Aluno não encontrado.");
                }

                Pedagogo pedagogoModel = await _context.Pedagogos.FindAsync(atendimento.codigoPedagogo);
                if (pedagogoModel == null)
                {
                    return NotFound("Pedagogo não encontrado.");
                }

                alunoModel.Situacao = EnumSituacaoMatricula.ATENDIMENTO_PEDAGOGICO;
                alunoModel.Atendimentos++;
                _context.Entry(alunoModel).State = EntityState.Modified;
                _context.Alunos.Update(alunoModel);
                await _context.SaveChangesAsync();

               
                pedagogoModel.Atendimentos++;
                _context.Entry(pedagogoModel).State = EntityState.Modified;
                _context.Pedagogos.Update(pedagogoModel);
                await _context.SaveChangesAsync();

                AlunoDTOResposta aluno = _mapper.Map<AlunoDTOResposta>(alunoModel);
                PedagogoDTOResposta pedagogo = _mapper.Map<PedagogoDTOResposta>(pedagogoModel);

                
                return Ok(new { aluno, pedagogo});
            }
            catch
            {
                return BadRequest("Operação inválida.");
            }

        }

    }
}

