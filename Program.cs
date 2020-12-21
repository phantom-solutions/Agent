using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Borealis_Server_Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Borealis Server Agent - DataTransfer Pipeline Test: \n");
            // Get the details from the user, and store them.
            Borealis_Agent agent = new Borealis_Agent();
            agent.Hostname = Dns.GetHostName();
            agent.Agent_IP = Dns.GetHostByName(agent.Hostname).AddressList[0].ToString();
            Console.Write("Enter JSON_DATA01: ");
            agent.JSON_DATA01 = Console.ReadLine();

            Console.Write("Enter JSON_DATA02: ");
            agent.JSON_DATA02 = Console.ReadLine();

            Console.Write("Enter JSON_DATA03: ");
            agent.JSON_DATA03 = Console.ReadLine();
            // Send the message
            byte[] bytes = sendMessage(System.Text.Encoding.Unicode.GetBytes(agent.ToJSON()));
            Console.WriteLine(cleanMessage(bytes));

            Console.Read();
        }

        private static byte[] sendMessage(byte[] messageBytes)
        {
            const int bytesize = 1024 * 1024;
            string borealisCP_Address = "";

            try
            {
                Console.WriteLine("Enter Borealis Control Panel IP Address:");
                borealisCP_Address = Console.ReadLine();
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(borealisCP_Address, 1234); // Create a new connection
                NetworkStream stream = client.GetStream();

                stream.Write(messageBytes, 0, messageBytes.Length); // Write the bytes
                Console.WriteLine("Connected to the server at: " + borealisCP_Address);
                Console.WriteLine("Waiting for server response...");

                messageBytes = new byte[bytesize];

                stream.Read(messageBytes, 0, messageBytes.Length);

                // Clean up
                stream.Dispose();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return messageBytes;
        }

        private static string cleanMessage(byte[] bytes)
        {
            string message = System.Text.Encoding.Unicode.GetString(bytes);
            Console.WriteLine(message);

            string messageToPrint = null;
            foreach (var nullChar in message)
            {
                if (nullChar != '\0')
                {
                    messageToPrint += nullChar;
                }
            }
            return messageToPrint;
        }
    }

    class Borealis_Agent
    {
        public string Hostname { get; set; }
        public string Agent_IP { get; set; }
        public string JSON_DATA01 { get; set; }
        public string JSON_DATA02 { get; set; }
        public string JSON_DATA03 { get; set; }

        // Create the JSON representation of object
        public string ToJSON()
        {
            string str = "{";
            str += "'hostname': '" + Hostname;
            str += "','agent_IP': '" + Agent_IP;
            str += "','json_data01': '" + JSON_DATA01;
            str += "','json_data02': '" + JSON_DATA02;
            str += "','json_data03': '" + JSON_DATA03;
            str += "'}";
            return str;
        }
    }
}
