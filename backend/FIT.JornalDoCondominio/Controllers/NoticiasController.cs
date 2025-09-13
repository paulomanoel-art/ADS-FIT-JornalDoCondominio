using FIT.JornalDoCondominio.Data;
using FIT.JornalDoCondominio.DTOs;
using FIT.JornalDoCondominio.Models;
using FIT.JornalDoCondominio.Services;
using Microsoft.AspNetCore.Mvc;

namespace FIT.JornalDoCondominio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SimpleAuth]
    public class NoticiasController : ControllerBase
    {
        private readonly JornalDoCondominioDbContext _context;

        public NoticiasController(JornalDoCondominioDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CriarNoticia([FromBody] NoticiaCreateDTO dto)
        {
            if (String.IsNullOrEmpty(dto.Titulo))
                return BadRequest("Informe o título da notícia!");

            if (String.IsNullOrEmpty(dto.Texto))
                return BadRequest("Informe o texto da notícia!");

            var noticia = new Noticia
            {
                Titulo = dto.Titulo,
                Texto = dto.Texto,
                FotoPath = dto.FotoPath,
                DataCriacao = DateTime.Now,
                UsuarioCriacaoId = dto.UsuarioCriacaoId,
                Ativo = true
            };

            _context.Noticias.Add(noticia);
            _context.SaveChanges();

            return Ok(noticia);
        }
    }
}
