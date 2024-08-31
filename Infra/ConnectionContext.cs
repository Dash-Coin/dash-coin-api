using System;
using coin_api.Domain.Model;
using Microsoft.EntityFrameworkCore;



public class ConnectionContext : DbContext
{

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // => optionsBuilder.UseNpgsql(
    //           "Server=localhost;" +
    //           "Port=5433;Database=coinapi;" +
    //           "User Id=user;" +
    //           "Password=123;");

    public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users"); // Certifique-se de que o nome da tabela esteja correto
        });

    }

}
