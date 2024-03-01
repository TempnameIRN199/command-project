﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Work.EDM;
using command_project.coding;

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
            //context.Skills.Add(new Work.EDM.Skill() { Name = "C++" });
            //context.Users.Add(new User()
            //{
            //    Name = "admin",
            //    SecondName = "admin",
            //    Login = "admin",
            //    Password = "a1d2m3i4n5",
            //    Country = "Empty",
            //    City = "Empty",
            //    PhoneNumber = "Empty",
            //    Email = "Empty",
            //    Status = "Admin",
            //    BirthDate = DateTime.Now,
            //    CreationDate = DateTime.Now
            //});
            ////DateTime birthDate = new DateTime(2006, 2, 20);
            ////Console.WriteLine(GetAge(birthDate));

            ////WorkContext context = new WorkContext();
            //context.Users.Add(new User()
            //{
            //    Name = "user1",
            //    SecondName = "user1",
            //    Login = "user1",
            //    Password = "1",
            //    Country = "Empty",
            //    City = "Empty",
            //    PhoneNumber = "Empty",
            //    Email = "Empty",
            //    Status = "Worker",
            //    BirthDate = DateTime.Now,
            //    CreationDate = DateTime.Now
            //});
            //context.Users.Add(new User()
            //{
            //    Name = "user2",
            //    SecondName = "user2",
            //    Login = "user2",
            //    Password = "1",
            //    Country = "Empty",
            //    City = "Empty",
            //    PhoneNumber = "Empty",
            //    Email = "Empty",
            //    Status = "Worker",
            //    BirthDate = DateTime.Now,
            //    CreationDate = DateTime.Now
            //});
            //context.Users.Add(new User()
            //{
            //    Name = "user3",
            //    SecondName = "user3",
            //    Login = "user3",
            //    Password = "1",
            //    Country = "Empty",
            //    City = "Empty",
            //    PhoneNumber = "Empty",
            //    Email = "Empty",
            //    Status = "Employer",
            //    BirthDate = DateTime.Now,
            //    CreationDate = DateTime.Now
            //});
            //context.SaveChanges();




            //WorkContext workContext = new WorkContext();
            //context.CVs.Add(new CV()
            //{
            //    UserId = context.Users.First(i => i.Name == "user1").Id,
            //    UserInfo = "Info1",
            //    Skills = "C#^5|SQL^1",
            //    CreationDate = DateTime.Now
            //});
            //context.CVs.Add(new CV()
            //{
            //    UserId = context.Users.First(i => i.Name == "user2").Id,
            //    UserInfo = "Info2",
            //    Skills = "C#^3|SQL^3",
            //    CreationDate = DateTime.Now
            //});
            //context.Requests.Add(new Request()
            //{
            //    Name = "request1",
            //    UserId = context.Users.First(i => i.Name == "user3").Id,
            //    RequestInfo = "Info2",
            //    Skills = "C#^3|SQL^3",
            //    CreationDate = DateTime.Now
            //});
            //context.RequestCVs.Add(new RequestCV()
            //{
            //    CreationDate = DateTime.Now,
            //    Status = "statusA",
            //    CVId = 1,
            //    RequestId = 1
            //});
            //context.RequestCVs.Add(new RequestCV()
            //{
            //    CreationDate = DateTime.Now,
            //    Status = "statusB",
            //    CVId = 2,
            //    RequestId = 1
            //});

            //context.SaveChanges();
            //Console.WriteLine("a");
            //workContext.Skills.Add(new Work.EDM.Skill { Name = "C#" });
            //workContext.Skills.Add(new Work.EDM.Skill { Name = "SQL" });
            //workContext.SaveChanges();
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
                            //rightPassword(0)>login(1)>newServerPort(2)>status(3)
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
                        string data = "wrongLogin";
                        SendData(data, int.Parse(texts[3]));
                    }
                }
                else if (texts[0] == "login")
                {
                    //login>login>password>name>secondName>email>number>country>city>birthdate>status>port

                    if (context.Users.ToList().Count(i => i.Login == texts[1]) == 0)
                    {
                        User tempUser = new User() 
                        { 
                            Login = texts[1], Password = texts[2],
                            Name = texts[3], SecondName = texts[4],
                            Email = texts[5], PhoneNumber = texts[6],
                            Country = texts[7], City = texts[8],
                            BirthDate = DateTime.Parse(texts[9]),
                            Status = texts[10],
                            CreationDate = DateTime.Now
                            //Age = int.Parse(texts[9]), Status = texts[10]
                        };

                        context.Users.Add(tempUser);
                        context.SaveChanges();

                        string data = "rightLogin";
                        SendData(data, int.Parse(texts[11]));
                    }
                    else
                    {
                        string data = "existLogin";
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
                List<string> list1 = new List<string> { "Accepted", "Accepted byAdmin" };
                List<string> list2 = new List<string> { "Declined", "Declined byAdmin" };
                List<string> list3 = new List<string> { "Accepted", "Declined", "Under Review" };

                string sendIt = "infoForAdmin>";
                WorkContext context = new WorkContext();
                sendIt += string.Join("|", context.Skills.Select(i => i.Name).ToArray());
                sendIt += ">" + context.RequestCVs.Count() + ">" + context.RequestCVs.Count(i => list1.Contains(i.Status))
                    + ">" + context.RequestCVs.Count(i => list2.Contains(i.Status)) + ">" + context.RequestCVs.Count(i => list3.Contains(i.Status))
                    + ">" + context.Users.Count(i => i.Status == "Worker") + ">" + context.Users.Count(i => i.Status == "Employer");
                SendData(sendIt);
            }
            else if (texts[0] == "getWorker")
            {
                string sendIt = "infoForWorker>";
                WorkContext context = new WorkContext();
                User user = context.Users.ToList().First(i=>i.Login == texts[1]);
                //sendIt += string.Join("|", context.Skills.Select(i => i.Name).ToArray());
                //login(1)>status(2)>name(3)>secondname(4)>email(5)>CreationDate(6)>BirthDate(7)>Country(8)>City(9)>Phonenumber(10)>age(11)
                sendIt += user.Login + ">" + user.Status + ">" + user.Name + ">" + user.SecondName + ">" + user.Email + ">" + user.CreationDate.ToString() + ">" + user.BirthDate.ToString() + ">" + user.Country + ">" + user.City + ">" + user.PhoneNumber + ">" + Functions.GetAge(user.BirthDate).ToString();
                SendData(sendIt);
            }
            else if (texts[0] == "getAdminTab1Info")
            {
                int page = int.Parse(texts[1]);
                List<string> list = new List<string>();
                Console.WriteLine(texts[2]);

                switch (texts[2])
                {
                    case "Всі":
                        {
                            list = new List<string> { "Accepted", "Declined", "Under Review", "Accepted byAdmin", "Declined byAdmin" };
                        }
                        break;

                    case "Підтверджені":
                        {
                            list = new List<string> { "Accepted", "Accepted byAdmin" };
                        }
                        break;

                    case "Відхилені":
                        {
                            list = new List<string> { "Declined", "Declined byAdmin" };
                        }
                        break;

                    case "Потребують підтвердження":
                        {
                            list = new List<string> { "Accepted", "Declined", "Under Review" };
                        }
                        break;
                }

                WorkContext context = new WorkContext();
                string sendMe = "adminTab1Info>" + string.Join(">", context.RequestCVs.Where(t => list.Contains(t.Status)).OrderBy(t => t.RequestId)
                    .Select(t => t.CV.User.Name + " " + t.CV.User.SecondName + "&" + t.CV.UserInfo + "&" + t.CV.Skills + "&" +
                    t.Request.Name + "&" + t.Request.RequestInfo + "&" + t.Request.Skills + "&" + t.Status + "&" + t.Request.IsActive + "&" + t.Id + "&" +
                    t.CV.User.PhoneNumber + "&" + t.CV.User.Email).Skip((page - 1) * 20).Take(20));
                SendData(sendMe);
                //Console.WriteLine(sendMe);
                Console.WriteLine();
            }
            else if (texts[0] == "acceptByAdmin")
            {
                int tId = int.Parse(texts[1]);
                WorkContext context = new WorkContext();
                context.RequestCVs.First(i => i.Id == tId).Status = "Accepted byAdmin";
                context.RequestCVs.First(i => i.Id == tId).Request.IsActive = false;
                context.SaveChanges();
                SendData("reload");
            }
            else if (texts[0] == "declineByAdmin")
            {
                int tId = int.Parse(texts[1]);
                WorkContext context = new WorkContext();
                context.RequestCVs.First(i => i.Id == tId).Status = "Declined byAdmin";
                context.SaveChanges();
                SendData("reload");
            }
            else if (texts[0] == "addSkill")
            {
                WorkContext context = new WorkContext();
                context.Skills.Add(new Work.EDM.Skill { Name = texts[1] });
                context.SaveChanges();

                string sendIt = "infoForAdmin>";
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