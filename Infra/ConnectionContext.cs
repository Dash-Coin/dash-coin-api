using System;
using Microsoft.EntityFrameworkCore;

namespace coin_api.Infra
{
public class ConnectionContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    =>  optionsBuilder.UseNpgsql(
              "Server=localhost;" +
              "Port=5432;Database=coinapi;" +
              "User Id=user;" +
              "Password=123;");

}
}