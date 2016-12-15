using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IO;
using YamlDotNet.Serialization;

namespace docker_dotnet_coreos.Controllers
{
    public class HomeController : Controller
    {

    [Route("robots.txt", Name = "GetRobotsText")]
    public ContentResult RobotsText()
        {
          StringBuilder stringBuilder = new StringBuilder();

          stringBuilder.AppendLine("user-agent: *");
          stringBuilder.AppendLine("disallow: /error/");
          //stringBuilder.AppendLine("allow: /error/foo");
          //stringBuilder.Append("sitemap: ");
          //stringBuilder.AppendLine(this.Url.RouteUrl("GetSitemapXml", null, this.Request.Url.Scheme).TrimEnd('/'));

          return this.Content(stringBuilder.ToString(), "text/plain", Encoding.UTF8);
        }

    [Route("/GetCloudConfig/{mac?}", Name = "GetCloudConfigYml")]
    public IActionResult CloudConfigYml(string mac)
    {
      var cloud_config = new CloudConfig();

      var token = "<token>";
      
      cloud_config.coreos.etcd2.discovery = "https://discovery.etcd.io/" + token;
      cloud_config.coreos.etcd2.advertise_dash_client_dash_urls = "http://$public_ipv4:2379";
      cloud_config.coreos.etcd2.initial_dash_advertise_dash_peer_dash_urls = "http://$private_ipv4:2380";
      cloud_config.coreos.etcd2.listen_dash_client_dash_urls = "http://0.0.0.0:2379,http://0.0.0.0:4001";
      cloud_config.coreos.etcd2.listen_dash_peer_dash_urls = "http://$private_ipv4:2380,http://$private_ipv4:7001";

      cloud_config.InitSshAuthKey();

      var ssh_key = "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAgEAxrht21lxTFLncowgatSVn/O8LftHB6prAqsdod/9K7c/wFw94/8MO2GhC06nt00jroq76RDDl/f/9H4eZapoC7umyGdUW4bzs/ultAwH8+aWGfOag7ic0Hwv/Fl3YIPmFC/dot6Pc3rY9whwR4QtV9sDlfDPgskVfTWAID987d6KYYl4QxdlKopuWkXFZ5LrMWuz/oaVu6LHSHS4sCx1DfNYEw+SHBGk6loEEHkHdZr3UvdnYSPagoE6bMm+cngSY0naWo57006ffDjdG8Oh6L4QmydpjjmRGQy7Wiz9FHDMTKjp8lDVxBg0e8j8WpvH/NyHU0ylgWHNi7toN/nqzfEnZ2V/X2EGaFX6pkdliPUCfkKsn4dSagWmDFN4rsfTUFmK9/yMbRbhyaT6/yQWqXqzVBPaFh/EXcONeYEr3jcTzFLHCae0UqHDGSDi2qMp4ixIHC0MuBkMJm0VQEECyVmBcTQ2q0ThNvofQTnhGnTvoi3wx9+TBH4dZAuhTa4ndEQSsogNmSCAvrCpMhSrdIrai2fUuOGUZt38BndqHLR54rTcBa/5iI4bjg+Rd7eQxseJT/zhQEmBdlALTCEjMLOvSbmkEX50vehWCCoR/RIySCU1/gdagAuCN8lZbD3qOIFzRXhamLjL0iD5MFqbGTh/hG2cBCvCqyHWpUIO3Ms= meredithk_ssh_rsa_4096";
              
      cloud_config.ssh_authorized_keys.Add(ssh_key);

      cloud_config.InitUsers();

      var user1 = new CloudConfigUser();

      user1.InitGroups();

      user1.name = "meredithk";
      user1.gecos = "Kevin Meredith";

      user1.groups.Add("sudo");
      user1.groups.Add("docker");

      user1.InitSshAuthKey();

      user1.ssh_authorized_keys.Add(ssh_key);

      cloud_config.users.Add(user1);

      var user2 = new CloudConfigUser();

      user2.InitGroups();

      user2.name = "rancher";
      user2.gecos = "Rancher System Account";

      user2.groups.Add("sudo");
      user2.groups.Add("docker");

      user2.InitSshAuthKey();

      user2.ssh_authorized_keys.Add(ssh_key);

      cloud_config.users.Add(user2);

      cloud_config.hostname = "coreos01";

      StringWriter strWriter = new StringWriter();
      String output;

      var serializer = new Serializer();
      serializer.Serialize(strWriter, cloud_config);

      output = mac;
      output += "\n\n";

      output += "#cloud-config\n\n";
      output += strWriter.ToString().
                Replace("_dash_", "-").
                Replace("$public_ipv4", "10.0.75.101").
                Replace("$private_ipv4","10.0.75.101");
          
      return this.Content(output, "text/yml", Encoding.UTF8);

    }         

    public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Evolutionary Networking Designs";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
