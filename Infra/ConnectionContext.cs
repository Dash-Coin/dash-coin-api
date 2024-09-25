using System;
using coin_api.Domain.Model;
using Microsoft.EntityFrameworkCore;



public class ConnectionContext : DbContext
{

    public DbSet<TransactionModel> Transactions { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<CategoryModel> Categories { get; set; }

    public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
       .HasMany(u => u.Transactions)
       .WithOne(t => t.User)
       .HasForeignKey(t => t.UserId);

        // Configurando relacionamento um para muitos entre User e Categories
        modelBuilder.Entity<User>()
            .HasMany(u => u.Categories)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        // Configurando relacionamento muitos-para-um entre TransactionModel e CategoryModel
        modelBuilder.Entity<TransactionModel>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoryId);

        base.OnModelCreating(modelBuilder);

    }

}
