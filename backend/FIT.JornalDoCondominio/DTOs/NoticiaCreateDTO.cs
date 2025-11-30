namespace FIT.JornalDoCondominio.DTOs
{
    public class NoticiaCreateDTO
    {
        public int? id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string FotoPath { get; set; }
        public int UsuarioCriacaoId { get; set; }
    }
}
