using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace Borealis_Agent
{
    public class AgentConnection
    {
        public static void EstablishConnection(string serverAddress)
        {
            //Query the server to verify that it exists and is responding to the agent with valid data.
            string URL = "http://" + serverAddress + ":5000";
            string urlParameters = "api/server/query";
            Console.WriteLine("{0} | Querying Control Panel Located at: {1}...", DateTime.Now, URL);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var queryResponse = response.Content.ReadAsAsync<ControlPanel>().Result;
                Console.WriteLine("{0} | Successfully Queried Control Panel at {1} at IP: {2}", DateTime.Now, queryResponse.HOSTNAME, queryResponse.IP);
            }
            else
            {
                Console.WriteLine("{0} | Unable to locate Borealis Control Panel at: {1}", DateTime.Now, URL);
                //Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            //Send authorization data to attempt to associate the agent with the Control Panel
            Console.Write("{0} | Please provide the authorization password to associate this agent with the control panel: ", DateTime.Now);
            Console.ReadLine();

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }

    public class AgentController : ApiController
    {
        public void API_Console_Update(string action, string details)
        {
            Console.WriteLine("{0} | Request: {1} was made by {2}", DateTime.Now, action, details);
        }

        public PayloadClass payloadInstance = new PayloadClass();

        public Agent GetAgent()
        {
            API_Console_Update("GetAgent()", "localhost");
            return new Agent
            {
                GUID = Guid.NewGuid().ToString(),
                IP = (Dns.GetHostAddresses(Dns.GetHostName())[1].ToString()),
                HOSTNAME = Dns.GetHostName()
            };
        }
        public string Test()
        {
            Console.WriteLine("Test");
            return "Test Command Received";
        }

        
        public void SetPayload(int id, [FromBody] string data)
        {
            if (id == 1337)
            {
                payloadInstance.PayloadData = data;
            }
        }

        public string GetPayload()
        {
            return payloadInstance.PayloadData;
        }

        // GET: http://localhost:8080/AgentAPI/Get
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: http://localhost:8080/AgentAPI/Get/5
        public string Get(int id)
        {
            int WebHook = id * 5;
            return WebHook.ToString();
        }

        // POST: http://localhost:8080/AgentAPI/Get
        public void Post([FromBody] string value)
        {
        }

        // PUT: http://localhost:8080/Agent/Put/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        public void Delete(int id)
        {
        }
    }
    public class Agent
    {
        public String GUID { get; set; }
        public String IP { get; set; }
        public String HOSTNAME { get; set; }
    }
    public class ControlPanel
    {
        public String IP { get; set; }
        public String HOSTNAME { get; set; }
    }

    public class PayloadClass
    {
        public String PayloadData { get; set; }
    }
}
