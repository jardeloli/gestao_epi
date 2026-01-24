namespace Gestão_Epi.Interfaces
{
    public interface IPerfilPermissaoService
    {
        Task AtribuirPermissaoAsync(int perfil_id, int permissao_id);
        Task RemoverPermissaoAsync(int perfil_id, int permissao_id);

        Task<IEnumerable<object>> ListarPermissoesPorPerfilAsync();
    }
}
