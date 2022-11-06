using Agencia.Models;
using Microsoft.EntityFrameworkCore;

namespace Agencia.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    Id = 1,
                    Nome = "Fabio Rodrigues",
                    Email = "fabiords@live.com",
                    Endereco = "rua caravelas ",
                    Cep = 25051230,
                    Destino = "Sao Paulo",
                    Quantidade = 2,
                    Formapag = "Boleto"
                },
                new Cliente

                {
                    Id = 2,
                    Nome = "Felipe Rodrigues",
                    Email = "feliperds@live.com",
                    Endereco = "rua guaruja ",
                    Cep = 25051230,
                    Destino = "Recife",
                    Quantidade = 4,
                    Formapag = "Cartão"
                }
                );
        }
    }
}
