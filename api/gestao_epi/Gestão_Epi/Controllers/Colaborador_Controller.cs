using Gestão_Epi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        
    }
}
