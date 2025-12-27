namespace Gestão_Epi.Models.Nova_Senha
{
    public class Nova_SenhaRequest
    {
        public string? EmailUsuario { get; set; }
        public string? NovaSenha { get; set; }
        public string? ConfirmarNovaSenha { get; set; }
    }
}
