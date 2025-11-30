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

            #region Enviar Notificação
            new SendMailService(_context.Assinatura.Where(w => w.Ativo == true).ToList(), noticia);
            #endregion

            return Ok(noticia);
        }

        [SimpleAuth]
        [HttpPut("editar-noticia")]
        public IActionResult EditarNoticia([FromBody] NoticiaCreateDTO dto)
        {
            if (String.IsNullOrEmpty(dto.Titulo))
                return BadRequest("Informe o título da notícia!");

            if (String.IsNullOrEmpty(dto.Texto))
                return BadRequest("Informe o texto da notícia!");

            var entity = _context.Noticias.Where(w => w.Id == dto.id).FirstOrDefault();


            entity.Titulo = dto.Titulo;
            entity.Texto = dto.Texto;
            entity.FotoPath = dto.FotoPath;
            entity.DataAlteracao = DateTime.Now;
            entity.UsuarioAlteracaoId = dto.UsuarioCriacaoId;

            _context.Noticias.Update(entity);
            _context.SaveChanges();

            return Ok();
        }

        [SimpleAuth]
        [HttpPost("cancelar-noticia")]
        public async Task<IActionResult> CancelarNoticia([FromBody] CancelarNoticiaDTO request)
        {
            var entity = _context.Noticias.Where(w => w.Id == request.id).FirstOrDefault();
            entity.Ativo = false;
            entity.DataAlteracao = DateTime.Now;
            entity.UsuarioAlteracaoId = request.usuarioCriacaoId;
            
            _context.Noticias.Update(entity);
            _context.SaveChanges();

            return await Noticias();
        }

        [SimpleAuth]
        [HttpGet("obter-noticia/{id}")]
        public async Task<IActionResult> ObterNoticia([FromRoute] int id)
        {
            var entity = _context.Noticias.Where(w => w.Id == id).FirstOrDefault();

            var response = new NoticiaResponseDTO
            {
                id = entity.Id,
                titulo = entity.Titulo,
                resumo = entity.Titulo,
                conteudoHtml = entity.Texto,
                dataNoticia = entity.DataCriacao,
                imagemUrl = entity.FotoPath
            };

            return Ok(response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Noticias()
        {
            var entity = _context.Noticias.ToList().Where(w => w.Ativo == true) .OrderByDescending(o => o.DataCriacao);

            var response = new List<NoticiaResponseDTO>();
            foreach(var noticia in entity)
            {
                response.Add(new NoticiaResponseDTO
                {
                    id = noticia.Id,
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
            _context.Assinatura.Add(new Assinatura { Email = registrarAssinaturaDTO.email, DataCriacao = DateTime.Now, Ativo = true });
            _context.SaveChanges();
            return Ok(new { mensagem = "Assinatura realizada com sucesso!" });
        }

        [HttpDelete("cancelar-assinatura/{id}")]
        [AllowAnonymous]
        public IActionResult CancelarAssinatura([FromRoute] int id)
        {
            var entity = _context.Assinatura.Find(id);
            entity.Ativo = false;
            entity.DataCancelamento = DateTime.Now;
            _context.SaveChanges();
            
            return Ok(new { mensagem = "Assinatura cancelada com sucesso!" });
        }
    }
}
