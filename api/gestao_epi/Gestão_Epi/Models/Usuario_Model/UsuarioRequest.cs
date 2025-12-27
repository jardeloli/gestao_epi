using Gestão_Epi.Entities;

namespace Gestão_Epi.Models.Usuario_Model
{
    public class UsuarioRequest
    {
        public int id { get; set; }
        public string nome { get; set; } = null!;
    }
}
