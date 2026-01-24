using Gestão_Epi.Data;
using Gestão_Epi.Entities;
using Gestão_Epi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gestão_Epi.Interfaces;

namespace Gestão_Epi.Controllers
{
    [Route("api/perfil")]
    [ApiController]
    public class Perfil_Gestao_Epi_Controller : ControllerBase
    {
        private readonly AppDbContext _bancoGE;

        private readonly IPerfilPermissaoService _perfilPermissaoService;
        public Perfil_Gestao_Epi_Controller(AppDbContext  bancoGE, IPerfilPermissaoService perfilPermissaoService)
        {
            _bancoGE = bancoGE;
            _perfilPermissaoService = perfilPermissaoService;
        }

        [HttpGet("listar-perfis")]
        public async Task<IActionResult> Listar_Perfis()
        {
            var perfils = await _bancoGE.perfil.Include(p => p.perfil_Permissao)
                .ThenInclude(pp => pp.permissao).Select(p => new PerfilResponse
                {
                    Id = p.id,
                    Nome = p.nome,
                    Descricao = p.descricao,
                    Permissoes = p.perfil_Permissao
                        .Select(pp => pp.permissao.codigo)
                        .ToList()
                }).ToListAsync()
            ;

            if (perfils == null || perfils.Count == 0)
            {
                return NotFound("Nenhum perfil cadastrado.");
            }

            return Ok(perfils);
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

        [HttpGet("listar-permissoes-por-perfil")]
        public async Task<IActionResult> ListarPermissoesPorPerfil()
        {
            var perfilPermissoes = await _perfilPermissaoService.ListarPermissoesPorPerfilAsync();
            return Ok(perfilPermissoes);
        }

        [HttpPost("atribuir-permissao")]
        public async Task<IActionResult> AtribuirPermissao(int perfil_id, int permissao_id)
        {
        
            await _perfilPermissaoService.AtribuirPermissaoAsync(perfil_id, permissao_id);
            return Ok("Permissão atribuída com sucesso.");
           
        }

        [HttpDelete("remover-permissao")]
        public async Task<IActionResult> RemoverPermissao(int perfil_id, int permissao_id)
        {
            await _perfilPermissaoService.RemoverPermissaoAsync(perfil_id, permissao_id);
            return Ok("Permissão removida com sucesso.");
        }
    }
}
