using AutoMapper;
using LabSchoolAPI.DTO;
using LabSchoolAPI.Enums;
using LabSchoolAPI.LabSchool;
using LabSchoolAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabSchoolAPI.Services.ProfessorService
{
    public class ProfessoresService: IProfessoresService
    {
        private readonly LabSchoolContext _context;
        private readonly IMapper _mapper;

        public ProfessoresService(LabSchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<ProfessorDTOResposta>>> GetProfessores()
        {
            var professores = await _context.Professores.ToListAsync();
            List<ProfessorDTOResposta> professoresDTO = _mapper.Map<List<ProfessorDTOResposta>>(professores);
            return professoresDTO;
        }

        public async Task<ActionResult<ProfessorDTOResposta>> GetProfessor(int codigo)
        {
            var professor = await _context.Professores.FindAsync(codigo);

            if (professor is null)
            {
                return null;
            }
            ProfessorDTOResposta professorDTO = _mapper.Map<ProfessorDTOResposta>(professor);
            return professorDTO;
        }


        public async Task<ActionResult<ProfessorDTOResposta>> PutProfessor(int codigo, ProfessorDTORequisicao professorDTO)
        {
            Professor professor = await _context.Professores.FindAsync(codigo);
            if (professor is null)
            {
                return null;
            }

            professor.Nome = professorDTO.Nome;
            professor.CPF = professorDTO.CPF;
            professor.DataNascimento = professorDTO.DataNascimento;
            professor.Telefone = professorDTO.Telefone;
            professor.Formacao = (EnumFormacaoAcademica)Enum.Parse(typeof(EnumFormacaoAcademica), professorDTO.Formacao.ToUpper());
            professor.Experiencia = (EnumExperiencia)Enum.Parse(typeof(EnumExperiencia), professorDTO.Experiencia.ToUpper());
            professor.Estado = (EnumEstado)Enum.Parse(typeof(EnumEstado), professorDTO.Estado.ToUpper());
            _context.Entry(professor).State = EntityState.Modified;
            _context.Professores.Update(professor);
            await _context.SaveChangesAsync();

            ProfessorDTOResposta professorDTOResposta = _mapper.Map<ProfessorDTOResposta>(professor);
            return professorDTOResposta;
        }

       
        public async Task<ActionResult<ProfessorDTOResposta>> PostProfessor(ProfessorDTORequisicao professorDTOPost)
        {
            Professor professor = _mapper.Map<Professor>(professorDTOPost);
            var professores = await _context.Professores.ToListAsync();

            var resultado = professores.Where(x => x.CPF == professorDTOPost.CPF).FirstOrDefault();
            if (resultado is not null)
            {
                return null;
            }

            _context.Entry(professor).State = EntityState.Added;
            await _context.SaveChangesAsync();

            var professorDTO = _mapper.Map<ProfessorDTOResposta>(professor);

            return professorDTO;
        }


        public async Task<ActionResult> DeleteProfessor(int codigo)
        {
            var professor = await _context.Professores.FindAsync(codigo);
            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();
            return null;

        }

    }
}
