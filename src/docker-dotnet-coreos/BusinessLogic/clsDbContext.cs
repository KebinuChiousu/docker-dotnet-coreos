using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace docker_dotnet_coreos
{
  public class DataContext : DbContext
  {
    public DataContext()
    {
      Database.Migrate();
    }

    public DbSet<ServerConfig> CoreOS { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder
       optionsBuilder)
    {
      string connectionStringBuilder = new
         SqliteConnectionStringBuilder()
      {
        DataSource = "coreos.db"
      }
      .ToString();

      var connection = new SqliteConnection(connectionStringBuilder);
      optionsBuilder.UseSqlite(connection);
    }
  }

}
