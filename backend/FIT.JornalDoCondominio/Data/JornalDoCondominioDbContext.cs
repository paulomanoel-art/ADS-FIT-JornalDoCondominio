using FIT.JornalDoCondominio.Models;
using Microsoft.EntityFrameworkCore;

namespace FIT.JornalDoCondominio.Data
{
    public class JornalDoCondominioDbContext : DbContext
    {
        public JornalDoCondominioDbContext(DbContextOptions<JornalDoCondominioDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Assinatura> Assinatura { get; set; }
    }
}
