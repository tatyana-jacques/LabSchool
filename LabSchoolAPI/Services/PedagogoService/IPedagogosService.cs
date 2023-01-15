using LabSchoolAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LabSchoolAPI.Services.PedagogoService
{
    public interface IPedagogosService
    {
        Task <ActionResult<IEnumerable<PedagogoDTOResposta>>> GetPedagogos();
        Task <ActionResult<PedagogoDTOResposta>> GetPedagogo(int codigo);
        Task <ActionResult<PedagogoDTOResposta>> PutPedagogo(int codigo, PedagogoDTORequisicao pedagogoDTO);
        Task <ActionResult<PedagogoDTOResposta>> PostPedagogo(PedagogoDTORequisicao pedagogoDTOPost);
        Task <ActionResult> DeletePedagogo(int codigo);


    }
}
