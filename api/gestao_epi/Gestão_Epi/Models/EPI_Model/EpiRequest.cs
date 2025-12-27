namespace Gestão_Epi.Models
{
    public class EpiRequest
    {
       
        public string nome { get; set; }
        public int ca { get; set; }
        public string tamanho { get; set; }
        public DateOnly? validade { get; set; }

        public string descricao { get; set; } 

    }
}
