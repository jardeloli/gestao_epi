using Gestão_Epi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestão_Epi.Controllers
{
    [Route("api/estoque")]
    [ApiController]
    public class Estoque_Controller : ControllerBase
    {
        private readonly AppDbContext _bancoGE;

        public Estoque_Controller(AppDbContext bancoGE)
        {
            _bancoGE = bancoGE;
        }

        [HttpGet("listar-estoque")]
        public async Task<IActionResult> Listar_Estoque()
        {
            var estoque = await _bancoGE.estoque.ToListAsync();

            return Ok(estoque);
        }
    }
}
