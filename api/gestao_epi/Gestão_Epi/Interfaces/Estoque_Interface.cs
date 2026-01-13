namespace Gestão_Epi.Interfaces
{
    public interface Estoque_Interface
    {
        Task EntradaAsync(int epiId, int quantidade);
        Task SaidaAsync(int epiId, int quantidade);
    }
}
