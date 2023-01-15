using AutoMapper;
using LabSchoolAPI.DTO;
using LabSchoolAPI.LabSchool;
using LabSchoolAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabSchoolAPI.Services.PedagogoService
{
    public class PedagogosService : IPedagogosService
    {
        private readonly LabSchoolContext _context;
        private readonly IMapper _mapper;

        public PedagogosService(LabSchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<PedagogoDTOResposta>>> GetPedagogos()
        {
            var pedagogos = await _context.Pedagogos.ToListAsync();
            List<PedagogoDTOResposta> pedagogosDTO = _mapper.Map<List<PedagogoDTOResposta>>(pedagogos);
            return pedagogosDTO;
        }


        public async Task<ActionResult<PedagogoDTOResposta>> GetPedagogo(int codigo)
        {
            var pedagogo = await _context.Pedagogos.FindAsync(codigo);

            if (pedagogo is null)
            {
                return null;
            }
            PedagogoDTOResposta pedagogoDTO = _mapper.Map<PedagogoDTOResposta>(pedagogo);
            return pedagogoDTO;
        }


        public async Task <ActionResult<PedagogoDTOResposta>> PutPedagogo(int codigo, PedagogoDTORequisicao pedagogoDTO)
        {
            Pedagogo pedagogo = await _context.Pedagogos.FindAsync(codigo);
            if (pedagogo is null)
            {
                return null;
            }

            pedagogo.Nome = pedagogoDTO.Nome;
            pedagogo.CPF = pedagogoDTO.CPF;
            pedagogo.DataNascimento = pedagogoDTO.DataNascimento;
            pedagogo.Telefone = pedagogoDTO.Telefone;
            _context.Entry(pedagogo).State = EntityState.Modified;
            _context.Pedagogos.Update(pedagogo);
            await _context.SaveChangesAsync();

            PedagogoDTOResposta pedagogoDTOResposta = _mapper.Map<PedagogoDTOResposta>(pedagogo);
            return pedagogoDTOResposta;
        }


        public async Task<ActionResult<PedagogoDTOResposta>> PostPedagogo(PedagogoDTORequisicao pedagogoDTOPost)
        {
            Pedagogo pedagogo = _mapper.Map<Pedagogo>(pedagogoDTOPost);
            var pedagogos = await _context.Pedagogos.ToListAsync();

            var resultado = pedagogos.Where(x => x.CPF == pedagogoDTOPost.CPF).FirstOrDefault();
            if (resultado is not null)
            {
                return null;
            }

            _context.Entry(pedagogo).State = EntityState.Added;
            await _context.SaveChangesAsync();

            var pedagogoDTO = _mapper.Map<PedagogoDTOResposta>(pedagogo);

            return pedagogoDTO;
        }

        public async Task <ActionResult> DeletePedagogo(int codigo)
        {
            var pedagogo = await _context.Pedagogos.FindAsync(codigo);
            _context.Pedagogos.Remove(pedagogo);
            await _context.SaveChangesAsync();
            return null;
           
        }

       
    }
}
