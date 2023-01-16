using AutoMapper;
using LabSchoolAPI.DTO;
using LabSchoolAPI.Enums;
using LabSchoolAPI.LabSchool;
using LabSchoolAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabSchoolAPI.Services.AlunoService
{
    public class AlunosService : IAlunosService
    {
        private readonly LabSchoolContext _context;
        private readonly IMapper _mapper;

        public AlunosService(LabSchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ActionResult<IEnumerable<AlunoDTOResposta>>> GetAlunos(string? situacao)
        {
            var alunos = await _context.Alunos.ToListAsync();

            if (situacao is not null)
            {
                var alunosQuery = alunos.Where(x => x.Situacao.ToString() == situacao.ToUpper());

                if (alunosQuery.Count() == 0)
                {
                    return null;

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

        public async Task<ActionResult<AlunoDTOResposta>> GetAluno(int codigo)
        {
            var aluno = await _context.Alunos.FindAsync(codigo);
            AlunoDTOResposta alunoDTO = _mapper.Map<AlunoDTOResposta>(aluno);

            if (aluno is null)
            {
                return null;
            }

            return alunoDTO;
        }

        public async Task<ActionResult<AlunoDTOResposta>> PutAluno(int codigo, AlunoDTOPutRequisicao alunoDTOPut)
        {
            Aluno aluno = await _context.Alunos.FindAsync(codigo);
            if (aluno is null)
            {
                return null;
            }
            aluno.Situacao = (EnumSituacaoMatricula)Enum.Parse(typeof(EnumSituacaoMatricula), alunoDTOPut.Situacao.ToUpper());
            _context.Entry(aluno).State = EntityState.Modified;
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();

            AlunoDTOResposta alunoDTO = _mapper.Map<AlunoDTOResposta>(aluno);
            return alunoDTO;
        }

        public async Task <Aluno> PostAluno(AlunoDTOPostRequisicao alunoDTOPost)
        {
            Aluno aluno = _mapper.Map<Aluno>(alunoDTOPost);
            var alunos = await _context.Alunos.ToListAsync();

            var resultado = alunos.Where(x => x.CPF == alunoDTOPost.CPF).FirstOrDefault();
            if (resultado is not null)
            {
                return null;
            }

            _context.Entry(aluno).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return aluno;
           
        }

        public async Task<ActionResult> DeleteAluno(int codigo)
        {
            var aluno = await _context.Alunos.FindAsync(codigo);
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return null;
        }
    }
}
