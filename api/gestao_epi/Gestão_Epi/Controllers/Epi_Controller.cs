using Gestão_Epi.Data;
using Gestão_Epi.Entities;
using Gestão_Epi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace Gestão_Epi.Controllers
{
    [Route("api/epi")]
    [ApiController]
    public class Epi_Controller : ControllerBase
    {
        private readonly AppDbContext _bancoGE;

        public Epi_Controller(AppDbContext bancoGE)
        {
            _bancoGE = bancoGE;
        }
        [HttpGet("listar-epis")]
        public async Task<IActionResult> Listar_Epis()
        {
            var epis = await _bancoGE.epi.Include(e =>e.estoque).ToListAsync();
            return Ok(epis);
        }

        [HttpPost("cadastrar-epi")]
        public async Task<IActionResult> Cadastrar_Epi([FromBody]EpiRequest request) 
        {
            var epi_existente = await _bancoGE.epi.FirstOrDefaultAsync(e => e.nome == request.nome);
                
            if(epi_existente != null)
            {
                return Conflict("EPI já cadastrado.");
            };

            if(string.IsNullOrWhiteSpace(request.nome) || !Epi.ChecarTamanhoCa(request.ca) || 
                string.IsNullOrWhiteSpace(request.tamanho) || string.IsNullOrWhiteSpace(request.descricao) || 
                request.validade == default(DateOnly) || string.IsNullOrWhiteSpace(request.cor)|| string.IsNullOrWhiteSpace(request.fabricante))
            {
                return BadRequest("Dados inválidos. Verifique os campos obrigatórios.");
            }

            var epi_novo = new Epi
            {
                nome = request.nome,
                ca = request.ca,
                tamanho = request.tamanho,
                validade = request.validade,
                descricao = request.descricao,
                cor = request.cor,
                fabricante = request.fabricante
            };

            var estoque_novo = new Estoque
            {
                epi = epi_novo,
                quantidade = 0
            };

            _bancoGE.epi.Add(epi_novo);
            _bancoGE.estoque.Add(estoque_novo);
            await _bancoGE.SaveChangesAsync();

            return Ok("EPI cadastrado com sucesso.");


        }

        [HttpPut("atualizar-epi/{id}")]
        public async Task<IActionResult> Atualizar_Epi(int id, [FromBody]EpiRequest request)
        {
            var epi = await _bancoGE.epi.FindAsync(id);

            if(epi == null)
            {
                return NotFound("EPI não encontrado.");
            }

            if(string.IsNullOrWhiteSpace(request.nome) || !Epi.ChecarTamanhoCa(request.ca) || 
                string.IsNullOrWhiteSpace(request.tamanho) || string.IsNullOrWhiteSpace(request.descricao) || 
                request.validade == default(DateOnly) || string.IsNullOrWhiteSpace(request.cor)|| string.IsNullOrWhiteSpace(request.fabricante))
            {
                return BadRequest("Dados inválidos. Verifique os campos obrigatórios.");
            }

            epi.nome = request.nome;
            epi.ca = request.ca;
            epi.tamanho = request.tamanho;
            epi.validade = request.validade;
            epi.descricao = request.descricao;
            epi.cor = request.cor;
            epi.fabricante = request.fabricante;

            await _bancoGE.SaveChangesAsync();
            return Ok("EPI atualizado com sucesso.");
        }

        [HttpDelete("deletar-epi/{id}")]
        public async Task<IActionResult> Deletar_Epi(int id)
        {
            var epi = await _bancoGE.epi.FindAsync(id);

            if(epi == null)
            {
                return NotFound("EPI não encontrado.");
            }

            _bancoGE.epi.Remove(epi);

            await _bancoGE.SaveChangesAsync();

            return Ok("EPI deletado com sucesso.");
        }
    }
}
