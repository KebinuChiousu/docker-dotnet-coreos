using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CloudConfig
{
  public CloudConfig()
  {
    coreos = new _coreos();
  }

  public void InitUsers()
  {
    users = new _users();
  }

  public void InitWriteFiles()
  {
    write_files = new List<_write_files>();
  }

  public void InitSshAuthKey()
  {
    ssh_authorized_keys = new List<string>();
  }

  public string hostname { get; set; }
  
  public List<string> ssh_authorized_keys { get; set; }

  public _users users { get; set; }

  public List<_write_files> write_files { get; set; }

  public _coreos coreos { get; set; }

  public class _users
  {

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

  public class _write_files
  {

    public _write_files()
    {
      content = new List<string>();
    }

    public string path { get; set; }
    public List<string> content { get; set; }
    public string permissions { get; set; }
    public string owner { get; set; }
    public string encoding { get; set; }
    
  }

  

  public class _coreos
  {

    public _coreos()
    {
      etcd2 = new _etcd2();      
    }

    public void InitFleet()
    {
      fleet = new _fleet();
    }

    public void InitFlannel()
    {
      flannel = new _flannel();
    }

    public _etcd2 etcd2 { get; set; }
    public _fleet fleet { get; set; }
    public _flannel flannel { get; set; }

    public class _etcd2
    {
      public string discovery { get; set; }
      public string advertise_dash_client_dash_urls { get; set; }
      public string initial_dash_advertise_dash_peer_dash_urls { get; set; }
      public string listen_dash_client_dash_urls { get; set; }
      public string listen_dash_peer_dash_urls { get; set; }
    }

    public class _fleet
    {
      public string agent_ttl { get; set; }
      public string engine_reconcile_interval { get; set; }
      public string etcd_cafile { get; set; }
      public string etcd_certfile { get; set; }
      public string etcd_key_prefix { get; set; }
      public string etcd_request_timeout { get; set; }
      public string etcd_server { get; set; }
      public string metadata { get; set; }
      public string public_ip { get; set; }
      public int verbosity { get; set; }
    }

    public class _flannel
    {
      public string etcd_endpoints { get; set; }
      public string etcd_cafile { get; set; }
      public string etcd_certfile { get; set; }
      public string etcd_keyfile { get; set; }
      public string etcd_prefix { get; set; }
      public string ip_masq { get; set; }
      public string subnet_file { get; set; }
      public string @interface {get; set;}
      public string public_ip { get; set; }
    }

    public class _locksmith
    {
      public string endpoint { get; set; }
      public string etcd_cafile { get; set; }
      public string etcd_certfile { get; set; }
      public string etcd_keyfile { get; set; }
      public string group { get; set; }
      public string window_start { get; set; }
      public string window_length { get; set; }
    }

    public class _update
    {
      public string reboot_dash_strategy { get; set; }
      public string server { get; set; }
      public string group { get; set; }
    }

    public class _units
    {
      public _units()
      {
        mask = false;
      }

      public void InitContent()
      {
        content = new List<string>();
      }

      public void InitDropIns()
      {
        drop_dash_ins = new List<drop_ins>();
      }

      public string name { get; set; }
      public Boolean runtime { get; set; }
      public Boolean enable { get; set; }
      public List<string> content { get; set; }
      public string command { get; set; }
      public Boolean mask { get; set; }
      public List<drop_ins> drop_dash_ins { get; set; }

    }
    
  }    
       
}

public class drop_ins
{
  public drop_ins()
  {
    content = new List<string>();
  }

  public string name { get; set; }
  public List<string> content { get; set; }
}


public class CloudConfigBuilder
{

  void BuildCloudConfig()
{

  var cloud_config = new
  {
    ssh_authorized_keys = new[]
        {
          "ssh-rsa"
        },
    hostname = "coreos01",
    users = new[]
        {
          new
          {
            name = "meredithk",
            gecos = "Kevin Meredith",
            groups = new []
            {
              "sudo",
              "docker"
            },
            ssh_authorized_keys = new []
                {
                   "ssh-rsa"
                }

          },
          new
          {
            name = "rancher",
            gecos = "Rancher System Account",
            groups = new []
            {
              "sudo",
              "docker"
            },
            ssh_authorized_keys = new []
                {
                   "ssh-rsa"
                }

          }

        }

  };

}


}