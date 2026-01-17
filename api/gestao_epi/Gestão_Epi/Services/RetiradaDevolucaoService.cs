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

        
        public async Task<IEnumerable<object>> ListarAsync()
        {
            var registros = await _bancoGE.retirada_devolucao
                .Include(rd => rd.usuario)
                .Include(rd => rd.colaborador)
                .Include(rd => rd.visitante)
                .Include(rd => rd.epi)
                .Select(rd => new
                {
                    rd.id,
                    Usuario = rd.usuario.nome,
                    Colaborador = rd.colaborador != null ? rd.colaborador.nome : null,
                    Visitante = rd.visitante != null ? rd.visitante.nome : null,
                    Epi = rd.epi.nome,
                    rd.quantidade,
                    rd.data_retirada,
                    rd.data_devolucao,
                    rd.justificativa_retirada,
                    rd.justificativa_devolucao
                })
                .ToListAsync();
            return registros.Cast<object>();
        }

        public async Task<object> BuscarPorIdAsync(int id)
        {
            var registro = await _bancoGE.retirada_devolucao.FirstOrDefaultAsync(rd => rd.id == id);

            if (registro == null)
            {
                throw new InvalidOperationException("Registro de retirada/devolução não encontrado");
            }

            var retirada_devolucao = await _bancoGE.retirada_devolucao
                .Include(registro => registro.id == id)
                .Select(rd => new
                {
                    rd.id,
                    Usuario = rd.usuario.nome,
                    Colaborador = rd.colaborador != null ? rd.colaborador.nome : null,
                    Visitante = rd.visitante != null ? rd.visitante.nome : null,
                    Epi = rd.epi.nome,
                    rd.quantidade,
                    rd.data_retirada,
                    rd.data_devolucao,
                    rd.justificativa_retirada,
                    rd.justificativa_devolucao
                })
                .ToListAsync();

            return retirada_devolucao.Cast<object>();
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

            var registros = new Retirada_devolucao
            {
                usuario = usuario,
                colaborador = colaborador,
                visitante = visitante,
                epi_id = epi_id,
                quantidade = quantidade,
                data_retirada = DateTime.Now,
                justificativa_retirada = justificativa_retirada
            };

            _bancoGE.retirada_devolucao.Add(registros);
            await _bancoGE.SaveChangesAsync();
        }

        public async Task RegistrarDevolucaoAsync(int usuario_id, int retirada_devolucao_id, int quantidade, string? justificativa_devolucao)
        {
            var registros = await _bancoGE.retirada_devolucao.FindAsync(retirada_devolucao_id);
            if (registros == null)
            {
                throw new InvalidOperationException("Registro de retirada/devolução não encontrado");
            }
            if (registros.data_devolucao != null)
            {
                throw new InvalidOperationException("Este item já foi devolvido");
            }
            var estoque = await _bancoGE.estoque.FirstOrDefaultAsync(e => e.epi_id == registros.epi_id);
            if (estoque == null)
            {
                throw new InvalidOperationException("EPI não encontrado no estoque");
            }

            var visitante = await _bancoGE.visitante.FindAsync(registros.visitante_id);
            if (visitante != null)
            {
                estoque.quantidade += quantidade;
                registros.data_devolucao = DateTime.Now;
                registros.justificativa_devolucao = justificativa_devolucao;
                await _bancoGE.SaveChangesAsync();
            }
            
            registros.data_devolucao = DateTime.Now;
            registros.justificativa_devolucao = justificativa_devolucao;
            await _bancoGE.SaveChangesAsync();
        }
    }
}
