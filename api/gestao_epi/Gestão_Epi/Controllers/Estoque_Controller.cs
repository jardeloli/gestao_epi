using Gestão_Epi.Data;
using Gestão_Epi.Interface;
using Gestão_Epi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestão_Epi.Controllers
{
    [Route("api/estoque")]
    [ApiController]
    public class Estoque_Controller : ControllerBase
    {
        

        private readonly IEstoqueService _estoqueService;
        public Estoque_Controller(IEstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        [HttpGet("listar-estoque")]
        public async Task<IActionResult> Listar_Estoque()
        {
            var estoque = await _estoqueService.ListarEstoqueAsync();

            return Ok(estoque);
        }

        [HttpPost("entrada")]
        public async Task<IActionResult> Entrada(int id, int quantidade)
        {
            await _estoqueService.EntradaAsync(id, quantidade);
            return Ok("Entrada registrada com sucesso");

        }

        [HttpPost("saida")]
        public async Task<IActionResult> Saida(int id, int quantidade)
        {
            await _estoqueService.SaidaAsync(id, quantidade);
            return Ok("Saída registrada com sucesso");
        }
    }
}
