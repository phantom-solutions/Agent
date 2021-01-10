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
    public class AgentAPIController : ApiController
    {
        public PayloadClass payloadInstance = new PayloadClass();

        public Agent GetAgent()
        {
            return new Agent
            {
                AgentGUID = "d6871a01-ca61-4754-9dcb-0db774fda886",
                AgentIP = "000.000.0.0",
                AgentHOSTNAME = "Dummy Hostname"
            };
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

        // PUT: http://localhost:8080/AgentAPI/Put/5
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
