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
            var config = new HttpSelfHostConfiguration("http://localhost:8080");

            config.Routes.MapHttpRoute(
                name: "API",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            using (HttpSelfHostServer agentAPIserver = new HttpSelfHostServer(config))
            {
                agentAPIserver.OpenAsync().Wait();
                Console.WriteLine("{0} | Borealis Server Agent - API controller started and listening on http://localhost:8080", DateTime.Now);
                Console.Write("{0} | Please enter the IP address of the Borealis Control Panel (Leave blank to use localhost): ", DateTime.Now);
                string BorealisServerIP = Console.ReadLine();

                if (BorealisServerIP == "")
                {
                    AgentConnection.EstablishConnection("localhost");
                }
                else
                {
                    AgentConnection.EstablishConnection(BorealisServerIP);
                }
                Console.WriteLine("Press any key to terminate the agent. (This is normal)");
                Console.ReadLine();
            }
        }
    }
}
