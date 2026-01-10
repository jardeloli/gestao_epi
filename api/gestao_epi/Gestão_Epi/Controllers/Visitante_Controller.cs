using Gestão_Epi.Data;
using Gestão_Epi.Entities;
using Gestão_Epi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestão_Epi.Controllers
{
    [Route("api/visitante")]
    [ApiController]
    public class Visitante_Controller : ControllerBase
    {
        private readonly AppDbContext _bancoGE;

        public Visitante_Controller(AppDbContext bancoGE)
        {
            _bancoGE = bancoGE;
        }

        [HttpGet("listar-visitantes")]
        public async Task<IActionResult> Listar_Visitsantes()
        {
            var visitante = await _bancoGE.visitante.ToListAsync();

            if (visitante == null || visitante.Count == 0)
            {
                return NotFound ("Nenhum visitante encontrado.");
            }

            return Ok(visitante);
        }

        [HttpPost("cadastrar-visitante")]
        public async Task<IActionResult> Cadastrar_Visitante(VisitanteRequest request)
        {
            if (request.documento.Length > 11)
            {
                return BadRequest("Documento inválido.");
            }
            
            if(string.IsNullOrWhiteSpace(request.nome) || string.IsNullOrWhiteSpace(request.documento))
            {
                return BadRequest("Campos obrigatório o preenchimento.");
            }

            
            var visitante_existente = await _bancoGE.visitante.FirstOrDefaultAsync(v => v.documento == request.documento);

            

            if(visitante_existente != null)
            {
                return Conflict("Já existe um visitante cadastrado com esse documento.");
            }

            var visitante_novo = new Visitante
            (  request.nome,
               request.documento

            );
            
            _bancoGE.visitante.Add(visitante_novo);

            await _bancoGE.SaveChangesAsync();

            return Ok($"Visitante {visitante_novo.nome} cadastrado com sucesso!");
        }

        [HttpPut("atualizar-visitante/{id}")]
        public async Task<IActionResult> Atualizar_Visitante( int id, [FromBody] VisitanteRequest request)
        {
            var visitante = await _bancoGE.visitante.FirstOrDefaultAsync(v => v.id == id);

            if(visitante == null)
            {
                return NotFound("Visitante não encontrado.");
            }

            if (string.IsNullOrWhiteSpace(request.nome) || string.IsNullOrWhiteSpace(request.documento))
            {

                return BadRequest("Campos obrigatório o preenchimento");
            };

            visitante.Atualizar_Dados(request.nome, request.documento);

            await _bancoGE.SaveChangesAsync();

            return Ok($"Visitante {visitante.nome} atualizado com sucesso!");

        }

        [HttpDelete("deletar-visitante/{id}")]
        public async Task<IActionResult> Deletar_Visitante(int id)
        {
            var visitante = await _bancoGE.visitante.FirstOrDefaultAsync(v => v.id == id);

            if (visitante == null)
            {
                return NotFound("Visitante não encontrado.");
            }

            _bancoGE.visitante.Remove(visitante);

            await _bancoGE.SaveChangesAsync();

            return Ok($"Visitante {visitante.nome} deletado com sucesso!");
        }
    }
}
