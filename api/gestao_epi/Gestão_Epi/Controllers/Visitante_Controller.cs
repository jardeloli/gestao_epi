using Gestão_Epi.Data;
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
    }
}
