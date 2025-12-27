using Gestão_Epi.Data;
using Gestão_Epi.Entities;
using Gestão_Epi.Models;
using Gestão_Epi.Models.Pessoa_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestão_Epi.Controllers
{
    [Route("api/colaborador")]
    [ApiController]
    public class Colaborador_Controller : ControllerBase
    {
        private readonly AppDbContext _bancoGE;

        public Colaborador_Controller(AppDbContext bancoGE)
        {
            _bancoGE = bancoGE;
        }
        [HttpGet("listar-colaboradores")]
        public async Task<IActionResult> Listar_Colaboradores()
        {
            var colaboradores = await _bancoGE.colaborador.ToListAsync();

            if (colaboradores == null || colaboradores.Count == 0)
            {
                return NotFound("Nenhum colaborador encontrado.");
            }
            return Ok(colaboradores);
        }

        [HttpPost("cadastrar-colaborador")]
        public async Task<IActionResult> Cadastrar_Colaborador([FromBody]ColaboradorRequest request)
        {
            var matricula_padronizada = request.matricula.Trim().ToLower();

            var colaborador_existente = await _bancoGE.colaborador.FirstOrDefaultAsync(c => c.matricula.ToLower() == matricula_padronizada);

            if (colaborador_existente != null)
            {
                return Conflict("Colaborador já cadastrado.");
            }
            
            var colaborador_novo = new Colaborador
            {
                nome = request.nome,
                setor = request.setor,
                matricula = request.matricula
            };

            _bancoGE.colaborador.Add(colaborador_novo);
            await _bancoGE.SaveChangesAsync();

            return Ok("Colaborador " + colaborador_novo.nome + " cadastrado com sucesso.");
        }

        [HttpDelete("deletar-colaborador/{id}")]
        public async Task<IActionResult> Deletar_Colaborador(int id)
        {
            var colaborador = await _bancoGE.colaborador.FindAsync(id);

            if (colaborador == null)
            {
                return NotFound("Colaborador não encontrado.");
            }

            _bancoGE.colaborador.Remove(colaborador);
            await _bancoGE.SaveChangesAsync();

            return Ok($"Colaborador {colaborador.nome} foi deletado com sucesso!");
        }
    }
}
