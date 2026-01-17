using Gestão_Epi.Entities;

namespace Gestão_Epi.Interfaces
{
    public interface INotificacaoService
    {
        Task RegistrarNotificacaoAsync(
            int visitante_id,
            int retirada_id,
            DateTime dataLimite,
            string mensagem
            );
       
    }
}
