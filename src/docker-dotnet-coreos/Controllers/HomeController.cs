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
      
      cloud_config.coreos.etcd2.discovery = "https://discovery.etcd.io/<token>";
      cloud_config.coreos.etcd2.advertise_dash_client_dash_urls = "http://$public_ipv4:2379";
      cloud_config.coreos.etcd2.initial_dash_advertise_dash_peer_dash_urls = "http://$private_ipv4:2380";
      cloud_config.coreos.etcd2.listen_dash_client_dash_urls = "http://0.0.0.0:2379,http://0.0.0.0:4001";
      cloud_config.coreos.etcd2.listen_dash_peer_dash_urls = "http://$private_ipv4:2380,http://$private_ipv4:7001";



      //cloud_config.hostname = "coreos01";
                  
      StringWriter strWriter = new StringWriter();
      String output;

      var serializer = new Serializer();
      serializer.Serialize(strWriter, cloud_config);

      string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

      //output = mac;
      //output += "\n\n";

      output = "#cloud-config\n\n";
      output += strWriter.ToString(). 
                Replace("_dash_", "-").
                Replace("$public_ipv4","74.100.8.20").
                Replace("$private_ipv4",remoteIpAddress);
          
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
