using Gestão_Epi.Data;
using Gestão_Epi.Entities;
using Gestão_Epi.Interfaces;

namespace Gestão_Epi.Services
{
    public class NotificacaoService: INotificacaoService
    {
        private readonly AppDbContext _bancoGE;

        public NotificacaoService(AppDbContext bancoGE)
        {
            _bancoGE = bancoGE;
        }

        public async Task RegistrarNotificacaoAsync(
        int visitante_id,
        int retirada_id,
        DateTime dataLimite,
        string mensagem)
        {
            var notificacao = new Notificacao
            {
                mensagem = mensagem,
                visitante_id = visitante_id,
                retirada_id = retirada_id,
                data_limite = dataLimite,
                status = "Pendente",
                data_devolucao = null,
                colaborador_id = null
            };

            _bancoGE.notificacao.Add(notificacao);
            await  _bancoGE.SaveChangesAsync();
        }
    }
}
