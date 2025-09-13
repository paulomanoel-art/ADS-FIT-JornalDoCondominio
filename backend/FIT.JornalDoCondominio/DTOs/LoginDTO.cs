using System.Text.Json.Serialization;

namespace FIT.JornalDoCondominio.DTOs
{
    public class LoginDTO
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("senha")]
        public string Senha { get; set; }
    }
}
