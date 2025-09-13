namespace FIT.JornalDoCondominio.Models
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string FotoPath { get; set; }
        public DateTime DataCriacao { get; set; }
        public int UsuarioCriacaoId { get; set; }
        public int? UsuarioAlteracaoId { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Ativo { get; set; }

        public Usuario UsuarioCriacao { get; set; }
        public Usuario UsuarioAlteracao { get; set; }
    }
}
