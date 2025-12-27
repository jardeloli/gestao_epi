using Gestão_Epi.Data;
using Gestão_Epi.Entities;
using Gestão_Epi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestão_Epi.Controllers
{
    [Route("api/perfil")]
    [ApiController]
    public class Perfil_Gestao_Epi_Controller : ControllerBase
    {
        private readonly AppDbContext _bancoGE;

        public Perfil_Gestao_Epi_Controller(AppDbContext bancoGE)
        {
            _bancoGE = bancoGE;
        }

        [HttpPost("cadastrar-perfil")]
        public async Task<IActionResult> CadastrarPerfil([FromBody] PerfilRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.nome))
            {
                return BadRequest("Nome do perfil é obrigatório.");
            }


            var nomePadronizado = request.nome.Trim().ToLower();

            var perfilExistente = await _bancoGE.perfil
                .FirstOrDefaultAsync(p => p.nome.ToLower() == nomePadronizado);

            if (perfilExistente != null)
            {
                return BadRequest("Perfil já cadastrado!");
            }

            var perfil = new Perfil
            {
                nome = request.nome,
                descricao = request.descricao
            };

            _bancoGE.perfil.Add(perfil);
            await _bancoGE.SaveChangesAsync();
            return CreatedAtAction(
                nameof(CadastrarPerfil),
                new { id = perfil.id },
                    perfil);
        }

        [HttpPatch("atualizar-perfil")]
        public async Task<IActionResult> AtualizarPerfil([FromBody] PerfilRequest request)
        {
            var perfilExistente = await _bancoGE.perfil
            .FirstOrDefaultAsync(p => p.nome == request.nome);

            if (perfilExistente == null)
            {
                return NotFound("Perfil não encontrado");
            }

            else
            {
                perfilExistente.nome = request.novo_nome;
                perfilExistente.descricao = request.nova_descricao;
            }


            await _bancoGE.SaveChangesAsync();

            return Ok("Perfil atualizado com sucesso!");

        }

        [HttpDelete("deletar-perfil/{id}")]
        public async Task<IActionResult> DeletarPerfil(int id) 
        {
            var perfilExistente = await _bancoGE.perfil.FindAsync(id);
            if(perfilExistente == null)
            {
                return NotFound("Perfil não encontrado");
            }

            _bancoGE.perfil.Remove(perfilExistente);
            await _bancoGE.SaveChangesAsync();

            return Ok("Perfil deletado com sucesso!");
        }
    }
}
