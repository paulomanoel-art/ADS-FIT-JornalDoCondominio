using FIT.JornalDoCondominio.Data;
using FIT.JornalDoCondominio.DTOs;
using FIT.JornalDoCondominio.Models;
using FIT.JornalDoCondominio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIT.JornalDoCondominio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class NoticiasController : ControllerBase
    {
        private readonly JornalDoCondominioDbContext _context;

        public NoticiasController(JornalDoCondominioDbContext context)
        {
            _context = context;
        }

        [SimpleAuth]
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

            var listaDestinatarios = _context.Assinatura.ToList().Where(w => w.Ativo == true).Select(s => s.Email);
            if (listaDestinatarios.Any())
                new SendMailService(listaDestinatarios.ToList());

            return Ok(noticia);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Noticias()
        {
            var entity = _context.Noticias.ToList().Where(w => w.Ativo == true) .OrderByDescending(o => o.DataCriacao);

            var response = new List<NoticiaResponseDTO>();
            foreach(var noticia in entity)
            {
                response.Add(new NoticiaResponseDTO
                {
                    titulo = noticia.Titulo,
                    resumo = noticia.Titulo,
                    conteudoHtml = noticia.Texto,
                    dataNoticia = noticia.DataCriacao,
                    imagemUrl = noticia.FotoPath
                });
            }

            return Ok(response);
        }

        [HttpPost("assinatura")]
        [AllowAnonymous]
        public IActionResult RegistrarAssinatura([FromBody] RegistrarAssinaturaDTO registrarAssinaturaDTO)
        {
            _context.Assinatura.Add(new Assinatura { Email = registrarAssinaturaDTO.email, DataCriacao = DateTime.Now });
            _context.SaveChanges();
            return Ok(new { mensagem = "Assinatura realizada com sucesso!" });
        }
    }
}
