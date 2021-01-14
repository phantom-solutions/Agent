using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Borealis_Agent
{
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
                AgentGUID = Guid.NewGuid().ToString(),
                AgentIP = (Dns.GetHostAddresses(Dns.GetHostName())[1].ToString()),
                AgentHOSTNAME = Dns.GetHostName()
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
        public String AgentGUID { get; set; }
        public String AgentIP { get; set; }
        public String AgentHOSTNAME { get; set; }
    }

    public class PayloadClass
    {
        public String PayloadData { get; set; }
    }
}
