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
    }
}
