namespace Gestão_Epi.Models.Cadastrar_Model
{
    public class CadastrarRequest
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? ConfirmarSenha { get; set; }
        public int Perfil_Id { get; set; }
    }
}
