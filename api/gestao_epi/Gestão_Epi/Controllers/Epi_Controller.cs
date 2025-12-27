using Gestão_Epi.Data;
using Gestão_Epi.Entities;
using Gestão_Epi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("cadastrar-epi")]
        public async Task<IActionResult> Cadastrar_Epi([FromBody]EpiRequest request) 
        {
            var epi_existente = await _bancoGE.epi.FirstOrDefaultAsync(e => e.nome == request.nome);
                
            if(epi_existente != null)
            {
                return Conflict("EPI já cadastrado.");
            };

            var epi_novo = new Epi
            {
                nome = request.nome,
                ca = request.ca,
                tamanho = request.tamanho,
                validade = request.validade,
                descricao = request.descricao
            };

            _bancoGE.epi.Add(epi_novo);
            await _bancoGE.SaveChangesAsync();

            return Ok("EPI cadastrado com sucesso.");


        }
    }
}
