using Gestão_Epi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestão_Epi.Controllers
{
    [Route("api/permissoes")]
    [ApiController]
    public class Permissoes_Controller : ControllerBase
    {
        private readonly AppDbContext _bancoGE;

        public Permissoes_Controller(AppDbContext bancoGE)
        {
            _bancoGE = bancoGE;
        }

        [HttpGet]
        public async Task<IActionResult> Listar_Permissoes()
        {
            var permissoes = await _bancoGE.permissao.ToListAsync();

            if (permissoes == null || permissoes.Count == 0)
            {
                return NotFound("Nenhuma permissão cadastrada.");
            }

            return Ok(permissoes);
        }
    }
}
