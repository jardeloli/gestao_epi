using Gestão_Epi.Data;
using Gestão_Epi.Entities;
using Gestão_Epi.Models.Cadastrar_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Gestão_Epi.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class Usuario_Gestao_Epi_Controller : ControllerBase
    {
        private readonly AppDbContext _bancoGE;

        public Usuario_Gestao_Epi_Controller(AppDbContext bancoGE)
        {
            _bancoGE = bancoGE;
        }

        [HttpPost("cadastrar-usuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CadastrarRequest request)
        {
            if (request.Senha != request.ConfirmarSenha)
            {
                return BadRequest("As senhas não conferem.");
            }

            var usuarioExistente = await _bancoGE.usuario
                .FirstOrDefaultAsync(u => u.email == request.Email);

            if (usuarioExistente != null)
            {
                return BadRequest("Email já cadastrado.");

            }
            
            var usuario = new Usuario
            {
                nome = request.Nome,
                email = request.Email,
                perfil_id = request.Perfil_Id
            };

            usuario.Defsenha(request.Senha);

            _bancoGE.usuario.Add(usuario);
            await _bancoGE.SaveChangesAsync();

            var response = new CadastrarResponse
            {
                Nome = usuario.nome,
                Email = usuario.email,
                PerfilId = usuario.perfil_id
            };
            return Ok(response);
        }

        [HttpDelete("deletar-usuario/{id}")]
        public async Task<IActionResult> DeletarUsuario(int id)
        {

            var usuario = await _bancoGE.usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _bancoGE.usuario.Remove(usuario);
            await _bancoGE.SaveChangesAsync();
            return Ok("Usuário deletado com sucesso!");
        }
    }
}
