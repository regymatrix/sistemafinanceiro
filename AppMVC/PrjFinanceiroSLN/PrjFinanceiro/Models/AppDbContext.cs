
using Microsoft.EntityFrameworkCore;

namespace PrjFinanceiro.Models

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Agencia> Agencia { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Escolaridade> Escolaridade { get; set; }
        public DbSet<Etnia> Etnia { get; set; }
        public DbSet<Bairro> Bairro { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
    }
}
