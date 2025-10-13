namespace FIT.JornalDoCondominio.DTOs
{
    public class NoticiaResponseDTO
    {
        public string titulo { get; set; }
        public string imagemUrl { get; set; }
        public string resumo { get; set; }
        public string conteudoHtml { get; set; }
        public DateTimeOffset dataNoticia { get; set; }
    }
}
