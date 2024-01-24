using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace Server
{   
    class UdpServer //first, отримання даних
    {
        private UdpClient udpServer;
        private Thread receiveThread; //асинхронне отримання даних
        public UdpServer(int port) //запуск receiveThread
        {
            udpServer = new UdpClient(port);
            receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.Start(); //слухає вхідні дані
        }
        private void ReceiveData() //безкінечне отримання даних юзерів
        {
            while (true)
            {
                try
                {
                    IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = udpServer.Receive(ref clientEndPoint);
                    string message = Encoding.UTF8.GetString(data);
                    ProcessReceiveData(message, clientEndPoint); //обробка отриманих даних
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        private void ProcessReceiveData(string message, IPEndPoint clientEndPoint) //вивід даних
        {
            Console.WriteLine($"Received from {clientEndPoint}: {message}");
        }
        public void StopServer()
        {
            udpServer.Close();
            receiveThread.Abort();
        }
    }
    class Program //last, ств UdpServer
    {
        static void Main(string[] args)
        {
            UdpServer udpServer = new UdpServer(8081); //прослуховування порту
            Console.WriteLine("UDP Server is running. Press Enter to exit.");
            Console.ReadLine();
            udpServer.StopServer();
        }
    }
}
