namespace Gestão_Epi.Interface
{
    public interface IEstoqueService
    {
        Task EntradaAsync(int id, int quantidade);
        Task SaidaAsync(int id, int quantidade);
        Task<List<object>> ListarEstoqueAsync();
    }
}
