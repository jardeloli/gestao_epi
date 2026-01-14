using Gestão_Epi.Data;
using Gestão_Epi.Entities;
using Gestão_Epi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestão_Epi.Services
{
    public class RetiradaDevolucaoService : IRetiradaDevolucaoService
    {
        private readonly AppDbContext _bancoGE;

        public RetiradaDevolucaoService(AppDbContext bancoGE)
        {
            _bancoGE = bancoGE;
        }

        public async Task RegistrarRetiradaAsync(int usuario_id, int? colaborador_id, int? visitante_id, int epi_id, int quantidade, string? justificativa_retirada)
        {
            if(quantidade <= 0)
            {
                throw new ArgumentException("Quantidade inválida");
            }
            
            var estoque = await _bancoGE.estoque.FirstOrDefaultAsync(e => e.epi_id == epi_id);

            if (estoque == null)
            {
                throw new InvalidOperationException("EPI não encontrado no estoque");
            }
            if (estoque.quantidade < quantidade)
            {
                throw new InvalidOperationException("Quantidade insuficiente em estoque");
            }   
            estoque.quantidade -= quantidade;

            var usuario = await _bancoGE.usuario.FindAsync(usuario_id);

            if (usuario == null)
            {
                throw new InvalidOperationException("Usuário não encontrado");
            }

            var colaborador = await _bancoGE.colaborador.FindAsync(colaborador_id);
            var visitante = await _bancoGE.visitante.FindAsync(visitante_id);

            if (colaborador_id == null && visitante_id == null)
            {
                throw new InvalidOperationException("Colaborador ou Visitante não encontrado");
            }

            else if (colaborador_id != null && visitante_id != null)
            {
                throw new InvalidOperationException("A retirada deve ser feita por um Colaborador ou um Visitante, não ambos.");
            }

            else if (colaborador_id != null && colaborador == null)
            {
                throw new InvalidOperationException("Colaborador não encontrado");
            }

            else if (visitante_id != null && visitante == null)
            {
                throw new InvalidOperationException("Visitante não encontrado");
            }

            var retiradaDevolucao = new Retirada_devolucao
            {
                usuario = usuario,
                colaborador = colaborador,
                visitante = visitante,
                epi_id = epi_id,
                quantidade = quantidade,
                data_retirada = DateTime.Now,
                justificativa_retirada = justificativa_retirada
            };

            _bancoGE.retirada_devolucao.Add(retiradaDevolucao);
            await _bancoGE.SaveChangesAsync();
        }

    }
}
