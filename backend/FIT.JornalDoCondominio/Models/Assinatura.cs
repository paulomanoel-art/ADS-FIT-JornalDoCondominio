namespace FIT.JornalDoCondominio.Models
{
    public class Assinatura
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCancelamento { get; set; }
    }
}
