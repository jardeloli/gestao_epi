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
            //definir claims. Claims são informações sobre o usuário que serão incluídas no token!
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Id", usuario.id.ToString()),
                new Claim("Username", usuario.nome),
                new Claim("Email", usuario.email)
            };

            var keyString = _config["AppSettings:Token"];

            if (string.IsNullOrEmpty(keyString))
            {
                throw new Exception("A chave de segurança não foi encontrada no appsettings.json.");
            }

            //gerar a chave de segurança usando a chave definida no appsettings.json
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyString));

            //gerar as credenciais do token usando a chave de segurança e o algoritmo HMAC SHA512 
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //definir o tempo de expiração do token 
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: cred
             );

            //gerar o token e retorná-lo
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUsuario([FromBody] LoginRequest request)
        {

            var usuario = await _bancoGE.usuario
                .Include(u => u.perfil)
                    .ThenInclude(p => p.perfil_Permissao)
                        .ThenInclude(pp => pp.permissao) 
                .FirstOrDefaultAsync(u => u.email == request.Email);


            if (usuario == null || !usuario.Verifsenha(request.Senha))
            {
                return Unauthorized("Email ou senha inválidos.");
            }
           
            var token = GerarToken(usuario);


            return Ok(new 
            { 
              Token = token,
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
