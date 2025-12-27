using Gestão_Epi.Data;
using Gestão_Epi.Entities;
using Gestão_Epi.Models;
using Gestão_Epi.Models.Cadastrar_Model;
using Gestão_Epi.Models.Nova_Senha;
using Gestão_Epi.Models.Usuario_Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Gestão_Epi.Controllers
{
    

    [Route("api/usuario")]
    [ApiController]
    public class AuthGestao_Epi_Controller : ControllerBase
    {
        private readonly AppDbContext _bancoGE;

        private readonly IConfiguration _config;

        public AuthGestao_Epi_Controller(AppDbContext bancoGE, IConfiguration config)
        {
            _bancoGE = bancoGE;
            _config = config;
        }

        private string GerarToken(Usuario usuario)
        {
           
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.id.ToString()),
                new Claim(ClaimTypes.Email, usuario.email!),
                new Claim(ClaimTypes.Role, usuario.perfil_id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        

        [HttpPost("login")]
        public async Task<IActionResult> LoginUsuario([FromBody] LoginRequest request)
        {
            var usuario = await _bancoGE.usuario
                .FirstOrDefaultAsync(u => u.email == request.Email);


            if (usuario == null || !usuario.Verifsenha(request.Senha))
            {
                return Unauthorized("Email ou senha inválidos.");
            }
            if (string.IsNullOrWhiteSpace(request.Senha))
            {
                return Unauthorized("Email ou senha inválidos.");
            }

            var token = GerarToken(usuario);

            return Ok(new 
            { Token = token,
              Nome = usuario.nome,
              Perfil_Id = usuario.perfil_id
            });
            
            
        }


        [Authorize]
        [HttpGet("teste-auth")]
        public IActionResult TesteAuth()
        {

            return Ok("PASSOU");

        }

        [Authorize(Roles = "1")]
        [HttpPatch("atualizar-senha")]
        public async Task<IActionResult> AtualizarSenha([FromBody] Nova_SenhaRequest request)
        {
            int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var usuario = await _bancoGE.usuario.
                FindAsync(usuarioId);


            if (request.NovaSenha != request.ConfirmarNovaSenha)
            {
                return BadRequest("As senhas não são iguais.");
            }

            
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            usuario.Defsenha(request.NovaSenha);

            await _bancoGE.SaveChangesAsync();

            return Ok("Senha atualizada com sucesso.");
        }

        
        
    }
}
