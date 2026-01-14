using Gestão_Epi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestão_Epi.Controllers
{
    [Route("api/retirada-devolucao")]
    [ApiController]
    public class Retirada_Devolucao_Controller : ControllerBase
    {
        private readonly IRetiradaDevolucaoService _retiradaDevolucaoService;

        public Retirada_Devolucao_Controller(IRetiradaDevolucaoService retiradaDevolucaoService)
        {
            _retiradaDevolucaoService = retiradaDevolucaoService;
        }

        [HttpPost("registrar-retirada")]
        public async Task<IActionResult> RegistrarRetirada(
            int usuario_id,
            int? colaborador_id,
            int? visitante_id,
            int epi_id,
            int quantidade,
            string? justificativa_retirada)
        {
            await _retiradaDevolucaoService.RegistrarRetiradaAsync(
                usuario_id,
                colaborador_id,
                visitante_id,
                epi_id,
                quantidade,
                justificativa_retirada);
            return Ok("Retirada registrada com sucesso");
        }
    }
}
