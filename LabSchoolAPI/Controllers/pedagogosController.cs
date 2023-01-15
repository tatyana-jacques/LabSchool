using LabSchoolAPI.DTO;
using LabSchoolAPI.Services.PedagogoService;
using Microsoft.AspNetCore.Mvc;


namespace LabSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedagogosController : ControllerBase
    {

       private readonly IPedagogosService _pedagogosService;

        public pedagogosController (IPedagogosService pedagogosService)
        {
            _pedagogosService = pedagogosService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedagogoDTOResposta>>> GetPedagogos()
        {
            return await _pedagogosService.GetPedagogos();

        }


        [HttpGet("{codigo}")]
        public async Task<ActionResult<PedagogoDTOResposta>> GetPedagogo(int codigo)
        {
            var resultado = await _pedagogosService.GetPedagogo(codigo);

            if (resultado is null)
            {
                return NotFound("Código de pedagogo inexistente.");
            }

            return resultado;
        }


        [HttpPut("{codigo}")]
        public async Task<ActionResult<PedagogoDTOResposta>> PutPedagogo(int codigo, PedagogoDTORequisicao pedagogoDTO)
        {
            try
            {
               var resultado = await _pedagogosService.PutPedagogo(codigo, pedagogoDTO);
                    if (resultado is null)
                {
                    return NotFound("Pedagogo não encontrado.");
                }
                    return resultado;
            }
            catch
            {
                return BadRequest("Operação não realizada.");
            }

        }


        [HttpPost]
        public async Task<ActionResult<PedagogoDTOResposta>> PostPedagogo(PedagogoDTORequisicao pedagogoDTOPost)
        {
            try
            {
                var resultado = await _pedagogosService.PostPedagogo(pedagogoDTOPost);
                if (resultado is null)
                {
                    return Conflict("CPF já registrado.");
                }
                return resultado;
            }

            catch
            {
                return BadRequest("Dados inválidos.");
            }
        }


        [HttpDelete("{codigo}")]
        public async Task<ActionResult> DeletePedagogo(int codigo)
        {
            try
            {
                var resultado = await _pedagogosService.DeletePedagogo(codigo);
                return NoContent();
            }
            catch
            {
                return NotFound("Pedagogo não encontrado.");

            }
        }

       
    }
}

