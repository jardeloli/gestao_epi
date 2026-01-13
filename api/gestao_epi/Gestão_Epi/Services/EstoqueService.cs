using Gestão_Epi.Interface;
using Gestão_Epi.Data;
using Microsoft.EntityFrameworkCore;
using Gestão_Epi.Entities;
namespace Gestão_Epi.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly AppDbContext _bancoGE;

        public EstoqueService(AppDbContext bancoGE)
        {
            _bancoGE = bancoGE;
        }

        public async Task<List<object>> ListarEstoqueAsync()
        {
           
            var estoque = await _bancoGE.estoque.Include(e => e.epi)
                .Select(e => new
                {
                    e.id,
                    e.quantidade,
                    EpiId = e.epi_id,
                    NomeEpi = e.epi.nome,
                    Tamanho= e.epi.tamanho,
                    Ca = e.epi.ca,
                    Descricao= e.epi.descricao,
                })
                .ToListAsync();

            if (estoque == null || !estoque.Any())
            {
                throw new InvalidOperationException("Nenhum item em estoque encontrado.");
            }

            return estoque.Cast<object>().ToList();
        }
        public async Task EntradaAsync(int id, int quantidade)
        {
            if(quantidade <= 0)
            {
                throw new ArgumentException("Quantidade inválida");
            }
                
            var estoque = await _bancoGE.estoque.FirstOrDefaultAsync(e => e.epi_id == id);

            if (estoque == null)
            {
                throw new InvalidOperationException("EPI não encontrado no estoque");
            }

            estoque.quantidade += quantidade;

            await _bancoGE.SaveChangesAsync();
        }
        public async Task SaidaAsync(int id, int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException("Quantidade inválida");
            }
               
            var estoque = await _bancoGE.estoque
                .FirstOrDefaultAsync(e => e.epi_id == id);

            if (estoque == null)
            {
                throw new Exception("Estoque não encontrado para este EPI");
            }


            if (estoque.quantidade < quantidade)
            {
                throw new Exception("Estoque insuficiente");
            }

                
            estoque.quantidade -= quantidade;

            await _bancoGE.SaveChangesAsync();
        }


    }
}
