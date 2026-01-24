using Gestão_Epi.Data;
using Gestão_Epi.Entities;
using Gestão_Epi.Interfaces;
using Microsoft.EntityFrameworkCore;
using ZstdSharp;

namespace Gestão_Epi.Services
{
    public class PerfilPermissaoService : IPerfilPermissaoService
    {
        private readonly AppDbContext _bancoGE;

        public PerfilPermissaoService(AppDbContext bancoGE)
        {
            _bancoGE = bancoGE;
        }

        public async Task<IEnumerable<object>> ListarPermissoesPorPerfilAsync()
        {
            var perfilPermissoes = await _bancoGE.perfil_permissao
                .Include(pp => pp.perfil)
                .Include(pp => pp.permissao)
                .Select(pp => new
                {
                    pp.perfil_id,
                    NomePerfil = pp.perfil.nome,
                    pp.permissao_id,
                    CodigoPermissao = pp.permissao.codigo
                })
                .ToListAsync();
            return perfilPermissoes.Cast<object>();
        }
        public async Task AtribuirPermissaoAsync(int perfil_id, int permissao_id)
        {
            var perfilPermissao_existente = await _bancoGE.perfil_permissao
                .AnyAsync(pp => pp.perfil_id == perfil_id && pp.permissao_id == permissao_id);

            if (perfilPermissao_existente)
            {
                throw new Exception("A permissão já está atribuída a este perfil.");
            }

            var novoPerfilPermissao = new Perfil_Permissao
            {
                perfil_id = perfil_id,
                permissao_id = permissao_id
            };

            await _bancoGE.perfil_permissao.AddAsync(novoPerfilPermissao);

            await _bancoGE.SaveChangesAsync();

        }

        public async Task RemoverPermissaoAsync(int perfil_id, int permissao_id)
        {
            var perfilPermissao_existente = await _bancoGE.perfil_permissao
                .AnyAsync(pp => pp.perfil_id == perfil_id && pp.permissao_id == permissao_id);
            if (!perfilPermissao_existente)
            {
                throw new Exception("A permissão não encontrada.");
            }

            _bancoGE.perfil_permissao.Remove(new Perfil_Permissao
            {
                perfil_id = perfil_id,
                permissao_id = permissao_id
            });
            await _bancoGE.SaveChangesAsync();
        }
    }
}
