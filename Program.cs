using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http.Routing;
using System.Web.Http;

namespace Borealis_Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Borealis Server Agent - Starting API Controller... \n");
            var config = new HttpSelfHostConfiguration("http://localhost:8080");

            config.Routes.MapHttpRoute(
                name: "API",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            using (HttpSelfHostServer agentAPIserver = new HttpSelfHostServer(config))
            {
                agentAPIserver.OpenAsync().Wait();
                Console.Write("Borealis Server Agent - API Controller Initialized and listening on http://localhost:8080\n");
                Console.ReadLine();
            }
        }
    }
}
