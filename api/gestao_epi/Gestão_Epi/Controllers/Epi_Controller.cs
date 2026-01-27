using Gestão_Epi.Data;
using Gestão_Epi.Entities;
using Gestão_Epi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

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
        [HttpGet("listar-epis")]
        public async Task<IActionResult> Listar_Epis()
        {
            var epis = await _bancoGE.epi.Include(e =>e.estoque).ToListAsync();
            if (epis == null || epis.Count == 0)
            {
                return NotFound("Nenhum EPI cadastrado.");
            }
            return Ok(epis);
        }

        [HttpPost("cadastrar-epi")]
        public async Task<IActionResult> Cadastrar_Epi([FromForm]EpiRequest request) 
        {
            var epi_existente = await _bancoGE.epi.FirstOrDefaultAsync(e => e.nome == request.nome.ToLower());
                
            if(epi_existente != null)
            {
                return Conflict("EPI já cadastrado.");
            };

            
            if(string.IsNullOrWhiteSpace(request.nome.ToLower()) || !Epi.ChecarTamanhoCa(request.ca) || 
                string.IsNullOrWhiteSpace(request.tamanho.ToLower()) || string.IsNullOrWhiteSpace(request.descricao.ToLower()) || 
                request.validade == default(DateOnly) || string.IsNullOrWhiteSpace(request.cor.ToLower())|| string.IsNullOrWhiteSpace(request.fabricante.ToLower()))
            {
                return BadRequest("Dados inválidos. Verifique os campos obrigatórios.");
            }

            string? imagemUrl = null;

            if(request.imagem != null)
            {
             
                var extensoes_permitidas = new [] { ".jpg", ".jpeg", ".png" };
                var extensao_arquivo = Path.GetExtension(request.imagem.FileName).ToLower();

                if(!extensoes_permitidas.Contains(extensao_arquivo))
                {
                    return BadRequest("Formato de imagem inválido. Formatos permitidos: .jpg, .jpeg, .png");
                }

                var pasta = Path.Combine("wwwroot", "imagens_epi");
                Directory.CreateDirectory(pasta);

                var nome_arquivo = $"{Guid.NewGuid()}{extensao_arquivo}";
                var caminho_completo = Path.Combine(pasta, nome_arquivo);

                using(var stream = new FileStream(caminho_completo, FileMode.Create))
                {
                    await request.imagem.CopyToAsync(stream);
                }

                imagemUrl = $"/imagens_epi/{nome_arquivo}";

            }

            var epi_novo = new Epi
            {
                nome = request.nome.ToLower(),
                ca = request.ca,
                tamanho = request.tamanho.ToLower(),
                validade = request.validade,
                descricao = request.descricao.ToLower(),
                cor = request.cor.ToLower(),
                fabricante = request.fabricante.ToLower(),
                imagemUrl = imagemUrl
            };

            var estoque_novo = new Estoque
            {
                epi = epi_novo,
                quantidade = 0
            };

            _bancoGE.epi.Add(epi_novo);
            _bancoGE.estoque.Add(estoque_novo);
            await _bancoGE.SaveChangesAsync();

            return Ok("EPI cadastrado com sucesso.");


        }

        [HttpPut("atualizar-epi/{id}")]
        public async Task<IActionResult> Atualizar_Epi(int id, [FromForm]EpiRequest request)
        {
            var epi = await _bancoGE.epi.FindAsync(id);

            if(epi == null)
            {
                return NotFound("EPI não encontrado.");
            }

            if(string.IsNullOrWhiteSpace(request.nome.ToLower()) || !Epi.ChecarTamanhoCa(request.ca) || 
                string.IsNullOrWhiteSpace(request.tamanho.ToLower()) || string.IsNullOrWhiteSpace(request.descricao.ToLower()) || 
                request.validade == default(DateOnly) || string.IsNullOrWhiteSpace(request.cor.ToLower())|| string.IsNullOrWhiteSpace(request.fabricante.ToLower()))
            {
                return BadRequest("Dados inválidos. Verifique os campos obrigatórios.");
            }

            epi.nome = request.nome.ToLower();
            epi.ca = request.ca;
            epi.tamanho = request.tamanho.ToLower();
            epi.validade = request.validade;
            epi.descricao = request.descricao.ToLower();
            epi.cor = request.cor.ToLower();
            epi.fabricante = request.fabricante.ToLower();
            epi.imagemUrl = epi.imagemUrl;

            await _bancoGE.SaveChangesAsync();
            return Ok("EPI atualizado com sucesso.");
        }

        [HttpDelete("deletar-epi/{id}")]
        public async Task<IActionResult> Deletar_Epi(int id)
        {
            var epi = await _bancoGE.epi.FindAsync(id);

            if(epi == null)
            {
                return NotFound("EPI não encontrado.");
            }

            _bancoGE.epi.Remove(epi);

            await _bancoGE.SaveChangesAsync();

            return Ok("EPI deletado com sucesso.");
        }
    }
}
