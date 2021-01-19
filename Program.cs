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
                //Startup Message
                Console.WriteLine("Borealis Agent v0.01");

                //Start the API Controller
                Console.Write("{0} | [API] Start API Controller on http://localhost:8080: ", DateTime.Now);
                agentAPIserver.OpenAsync().Wait();
                Console.Write("Done\n");

                //Check if database exists on disk, if not, create it
                Console.Write("{0} | [DATABASE] Checking if Agent SQLite database exists on disk: ", DateTime.Now);
                Database_Functions.InitializeDatabase();

                //QUERY THE DATABASE (DEBUG)
                Console.WriteLine("{0} | [DATABASE] Querying SQLite Database...", DateTime.Now);
                Database_Functions.AgentTestQuery();

                Console.WriteLine("{0} | [API] Please enter the IP address of the Borealis Control Panel (Leave blank to use localhost): ", DateTime.Now);
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
