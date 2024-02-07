using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Work.EDM;

namespace Server
{
    internal class Program
    {
        static int serverPort = 22885;

        static List<Connection> connections = new List<Connection>();

        static WorkContext context = new WorkContext();
        static void Main(string[] args)
        {
            //WorkContext context = new WorkContext();
            //context.Skills.Add(new Skill() { Name = "C#" });
            //context.SaveChanges();
            ThreadPool.QueueUserWorkItem(ReceiveData, 0);
            while (true) { }
        }

        public static int GetFreePort()
        {
            int port = 31000;
            while (true)
            {
                try
                {
                    UdpClient client = new UdpClient(port);
                    client.Close();
                    return port;
                }
                catch
                {
                    port++;
                }
            }
        }
        static void ReceiveData(object state)
        {
            while (true)
            {

                UdpClient client = new UdpClient(serverPort);
                IPEndPoint ipEnd = null;
                byte[] responce = client.Receive(ref ipEnd);
                string text = Encoding.Unicode.GetString(responce);
                List<string> texts = text.Split('>').ToList();

                if (texts[0] == "checkPassword")
                {
                    //checkPassword>login>password>port
                    if (context.Users.ToList().Where(i => i.Login == texts[1]).Count() > 0)
                    {
                        User thisUser = context.Users.ToList().First(i => i.Login == texts[1]);

                        if (thisUser.Password == texts[2])
                        {
                            int clientPort = int.Parse(texts[3]);
                            int serverPort = GetFreePort();
                            //rightPassword>login>newServerPort
                            string data = "rightPassword>" + thisUser.Login + ">" + serverPort + ">" + thisUser.Status;
                            SendData(data, int.Parse(texts[3]));

                            Connection tempCon = new Connection(clientPort, serverPort);
                            connections.Add(tempCon);
                            ThreadPool.QueueUserWorkItem(ConnectionServerClient, tempCon);
                        }
                        else
                        {
                            string data = "wrongPassword";
                            SendData(data, int.Parse(texts[3]));
                        }
                    }
                    else
                    {
                        string data = "wrongPassword";
                        SendData(data, int.Parse(texts[3]));
                    }
                }
                else if (texts[0] == "login")
                {
                    //login>login>password>name>secondName>email>number>country>city>age>status>port

                    if (context.Users.ToList().Count(i => i.Login == texts[1]) == 0)
                    {
                        User tempUser = new User() 
                        { 
                            Login = texts[1], Password = texts[2],
                            Name = texts[3], SecondName = texts[4],
                            Email = texts[5], PhoneNumber = texts[6],
                            Country = texts[7], City = texts[8],
                            Age = int.Parse(texts[9]), Status = texts[10]
                        };

                        context.Users.Add(tempUser);
                        context.SaveChanges();

                        string data = "rightLogin";
                        SendData(data, int.Parse(texts[11]));
                    }
                    else
                    {
                        string data = "wrongLogin";
                        SendData(data, int.Parse(texts[11]));
                    }
                }

                client.Close();
            }
        }

        static void SendData(string text, int clientPort)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);

            UdpClient client = new UdpClient();
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), clientPort);

            client.Send(bytes, bytes.Length, ipEnd);

            client.Close();

            Console.WriteLine(text);
        }

        static void ConnectionServerClient(object connection)
        {
            Connection con = (Connection)connection;
            while (con.isWorking)
            {
                con.ReceiveData();
            }
            connections.Remove(connections.First(i => i.conName == con.conName));
        }
    }

    class Connection
    {
        public string conName;
        public int ClientPort { get; set; }
        public int ServerPort { get; set; }

        public bool isWorking = true;
        public Connection(int clientPort, int serverPort)
        {
            ClientPort = clientPort;
            ServerPort = serverPort;
            conName = ClientPort.ToString() + "-" + ServerPort.ToString();
        }

        public void ReceiveData()
        {
            UdpClient client = new UdpClient(ServerPort);
            IPEndPoint ipEnd = null;
            byte[] responce = client.Receive(ref ipEnd);
            string text = Encoding.Unicode.GetString(responce);
            List<string> texts = text.Split('>').ToList();

            if (texts[0] == "getAdmin")
            {
                string sendIt = "infoForAdmin>";
                WorkContext context = new WorkContext();
                sendIt += string.Join("|", context.Skills.Select(i => i.Name).ToArray());
                SendData(sendIt);
            }
            else if (texts[0] == "close")
            {
                isWorking = false;
            }

            client.Close();
        }

        public void SendData(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);

            UdpClient client = new UdpClient();
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), ClientPort);

            client.Send(bytes, bytes.Length, ipEnd);

            client.Close();

            Console.WriteLine(text);
        }
    }
}