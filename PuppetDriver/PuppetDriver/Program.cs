﻿using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Puppetry.PuppetDriver.TcpSocket;

namespace Puppetry.PuppetDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = Configuration.ProcessComandLineArguments(args);

            Parallel.Invoke(() => BuildWebHost(settings).Run(), StartTcpListner);
        }

        private static IWebHost BuildWebHost(Dictionary<string, string> settings)
        {
            const string PortParameter = "p";

            string baseUrl = "http://localhost";
            string port = "7111";

            if (settings.Count > 0)
            {
                if (settings.ContainsKey(PortParameter))
                    port = settings[PortParameter];
            }

            return WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseUrls($"{baseUrl}:{port}/")
                .Build();
        }

        private static void StartTcpListner()
        {
            PuppetListener.StartListen();
        }
    }
}
