using FIT.JornalDoCondominio.Data;
using FIT.JornalDoCondominio.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FIT.JornalDoCondominio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly JornalDoCondominioDbContext _context;

        public UsuariosController(JornalDoCondominioDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.EmailLogin == dto.Email && u.PwdLogin == dto.Senha);

            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos");

            return Ok(usuario);
        }
    }
}
