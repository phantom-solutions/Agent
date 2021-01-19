using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Borealis_Agent
{
    class Database_Functions
    {
        //Define the name of the SQLite database file
        public static string DB_File = "AgentDB.sqlite";

        public static void InitializeDatabase()
        {
            //Check if the SQLite database file already exists, and create it if it doesn't
            if (File.Exists(DB_File))
            {
                Console.Write("Database Already Exists.\n");
            }
            else
            {
                Console.Write("Database does not exist.  Creating Database...\n");
                SQLiteConnection.CreateFile(DB_File);
                CreateTableOutline();
                GenerateAgentData();
            }
        }

        public static void CreateTableOutline()
        {
            //Connect to the database
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={DB_File};Version=3;");
            m_dbConnection.Open();

            Console.WriteLine("{0} | [DATABASE] Creating Borealis Control Panel Data Table", DateTime.Now);
            //Create Control Panel Table Data
            string sql = "CREATE TABLE borealis_data (ip_address VARCHAR(15), guid VARCHAR(36))";
            SQLiteCommand generateCPTable = new SQLiteCommand(sql, m_dbConnection);
            generateCPTable.ExecuteNonQuery();

            Console.WriteLine("{0} | [DATABASE] Creating Borealis Agent Data Table", DateTime.Now);
            //Create Agent Table Data
            sql = "CREATE TABLE agent_data (ip_address VARCHAR(15), guid VARCHAR(36))";
            SQLiteCommand generateAgentTable = new SQLiteCommand(sql, m_dbConnection);
            generateAgentTable.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        public static void GenerateAgentData()
        {
            //Connect to the database
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={DB_File};Version=3;");
            m_dbConnection.Open();

            //Pull the local agent data to be populated into the agent database table
            string GUID = Guid.NewGuid().ToString();
            string IP = (Dns.GetHostAddresses(Dns.GetHostName())[1].ToString());
            string HOSTNAME = Dns.GetHostName();
            string sql = $"insert into agent_data (ip_address, guid) values ('{IP}', '{GUID}')";
            
            //Insert the data into the agent table
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        public static void StoreCPData(string HOSTNAME, string IP, string GUID)
        {
            //Connect to the database
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={DB_File};Version=3;");
            m_dbConnection.Open();

            //Pull the local agent data to be populated into the agent database table
            string sql = $"insert into borealis_data (hostname, ip_address, guid) values ('{HOSTNAME}', '{IP}', '{GUID}')";

            //Insert the data into the agent table
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public static void AgentTestQuery()
        {
            //Connect to the database
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={DB_File};Version=3;");
            m_dbConnection.Open();

            string sql = "select * from agent_data";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("{0} | [DATABASE] Agent IP Address: " + reader["ip_address"] + "\n{0} | [DATABASE] Agent GUID: " + reader["guid"], DateTime.Now);
            m_dbConnection.Close();
        }
    }
}
