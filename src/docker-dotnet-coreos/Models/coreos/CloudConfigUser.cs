using System;
using System.Collections.Generic;

public class CloudConfigUser
{

  public void InitGroups()
  {
    groups = new List<string>();
  }

  public void InitSshAuthKey()
  {
    ssh_authorized_keys = new List<string>();
  }

  public string name { get; set; }
  public string gecos { get; set; }
  public string passwd { get; set; }
  public string homedir { get; set; }
  public Boolean no_dash_create_dash_home { get; set; }
  public string primary_dash_group { get; set; }
  public List<string> groups { get; set; }
  public Boolean no_dash_user_dash_group { get; set; }
  public List<string> ssh_authorized_keys { get; set; }
  public string system { get; set; }
  public Boolean no_dash_log_dash_init { get; set; }
  public string shell { get; set; }
}
