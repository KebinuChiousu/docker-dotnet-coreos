using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace docker_dotnet_coreos.database
{
  public class ServerContext : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite("Filename=./App_Data/ServerConfig.sqlite");
    }
  }

  public class User
  {
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string gecos { get; set; }
    public string passwd { get; set; }
    public string homedir { get; set; }
    public Boolean no_create_home { get; set; }
    public string primary_dash_group { get; set; }
    public List<Group> groups { get; set; }
    public Boolean no_user_group { get; set; }
    public List<string> ssh_authorized_keys { get; set; }
    public string system { get; set; }
    public Boolean no_log_init { get; set; }
    public string shell { get; set; }
    public string Url { get; set; }
        
  }

  public class Group
  {
    public int GroupID { get; set; }
    public int GroupName { get; set; }

    public int UserID { get; set; }
    public User User { get; set; }
  }
    
}