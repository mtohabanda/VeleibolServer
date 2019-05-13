using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Veleibol.Models;

namespace angular.Web.Models
{
    public class DataContext: DbContext
    {
        public DataContext() {

        }


        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration
                ["ConnectionStrings:DefaultConnection"]);
        }

        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Puntuacion> PuntuacionEquipos { get; set; }

    }
}
