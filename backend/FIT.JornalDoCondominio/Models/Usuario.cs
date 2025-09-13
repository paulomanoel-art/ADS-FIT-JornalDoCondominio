namespace FIT.JornalDoCondominio.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public string EmailLogin { get; set; }
        public string PwdLogin { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
