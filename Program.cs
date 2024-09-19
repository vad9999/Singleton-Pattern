using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace singleton
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Servers server = Servers.Instance;
            while (true) 
            {
                Console.WriteLine("1 - Добавить сервер;");
                Console.WriteLine("2 - Получить список серверов, адреса которых начинаются с 'http';");
                Console.WriteLine("3 - Получить список серверов, адреса которых начинаются с 'https';");
                Console.WriteLine("Введите цифру:");
                switch(int.Parse(Console.ReadLine()))
                {
                    case 1:
                        server.AddServer(Console.ReadLine());
                        break;
                    case 2:
                        server.HTTP();
                        break;
                    case 3:
                        server.HTTPS();
                        break;
                }
            }
        }
    }
    public class Servers
    {
        private static object syncRoot = new Object();
        public List<string> server { get; set; }
        protected Servers()
        {
            server = new List<string>();
        }
        public static Servers Instance
        {
            get
            {
                return Nested.instance;
            }
        }
        public bool AddServer(string adr)
        {
            lock (syncRoot)
            {
                if ((adr.StartsWith("http:") || adr.StartsWith("https:")) && !server.Contains(adr))
                {
                    server.Add(adr);
                    return true;
                }
                else
                    return false;
            }
        }
        public void HTTP()
        {
            foreach (var serv in server)
            {
                if (serv.StartsWith("http:"))
                    Console.WriteLine(serv);
            }
        }
        public void HTTPS()
        {
            foreach (var serv in server)
            {
                if (serv.StartsWith("https:"))
                    Console.WriteLine(serv);
            }
        }
        private class Nested    
        {
            internal static readonly Servers instance = new Servers();
        }
    }
}