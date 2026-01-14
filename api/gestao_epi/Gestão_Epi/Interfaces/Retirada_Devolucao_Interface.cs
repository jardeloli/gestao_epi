namespace Gestão_Epi.Interfaces
{
    public interface IRetiradaDevolucaoService
    {
        Task RegistrarRetiradaAsync(
            int usuario_id,
            int? colaborador_id,
            int? visitante_id,
            int epi_id,
            int quantidade,
            string? justificativa_retirada
            );

        Task RegistrarDevolucaoAsync(
            int usuario_id,
            int retirada_devolucao_id,
            string? justificativa_devolucao
            );

        Task<IEnumerable<object>> ListarAsync();

        Task<object> BuscarPorIdAsync(int id);
    }
}
