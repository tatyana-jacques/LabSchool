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
                
                Aluno aluno = await _context.Alunos.FindAsync(atendimento.codigoAluno);
                if (aluno == null)
                {
                    return NotFound("Aluno não encontrado.");
                }

                Pedagogo pedagogo = await _context.Pedagogos.FindAsync(atendimento.codigoPedagogo);
                if (pedagogo == null)
                {
                    return NotFound("Pedagogo não encontrado.");
                }

                aluno.Situacao = EnumSituacaoMatricula.ATENDIMENTO_PEDAGOGICO;
                aluno.Atendimentos++;
                _context.Entry(aluno).State = EntityState.Modified;
                _context.Alunos.Update(aluno);
                await _context.SaveChangesAsync();

               
                pedagogo.Atendimentos++;
                _context.Entry(pedagogo).State = EntityState.Modified;
                _context.Pedagogos.Update(pedagogo);
                await _context.SaveChangesAsync();

                AlunoDTOResposta alunoDTO = _mapper.Map<AlunoDTOResposta>(aluno);
                PedagogoDTOResposta pedagogoDTO = _mapper.Map<PedagogoDTOResposta>(pedagogo);

          
                
                return Ok(new { alunoDTO, pedagogoDTO});
            }
            catch
            {
                return BadRequest("Operação inválida.");
            }

        }

    }
}

