using System;
using Microsoft.EntityFrameworkCore;



public class ConnectionContext : DbContext
{

    public DbSet<Transaction> Transactions { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql(
              "Server=localhost;" +
              "Port=5433;Database=coin-api;" +
              "User Id=user;" +
              "Password=123;");

}
