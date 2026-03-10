using Microsoft.EntityFrameworkCore;
using ZapasPractica.Models;

namespace ZapasPractica.Data
{
    public class ZapatillasContext : DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options) : base(options) { }
        public DbSet<Zapatilla> Zapatillas { get; set; }
        public DbSet<ImagenZapatilla> Imagenes { get; set; }
    }
}
