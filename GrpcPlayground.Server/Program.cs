using System.Collections.Generic;
using System.IO;

namespace GrpcPlayground.Server
{
    using System;
    using Grpc.Core;
    class Program
    {
        static void Main(string[] args)
        {
            const int Port = 50050;
            const string host = "DESKTOP-DERTRE3";
            var server = new Server
            {
                Ports = { new ServerPort(host, Port, GetCredentials()) },
                Services =
                {
                    Messages.BusinessUnitService.BindService(new BussinesUnitService()),
                    Messages.IWillSayHelloService.BindService(new IWillSayHelloService())
                }
            };
            server.Start();
            Console.WriteLine("Server up!");
            Console.ReadKey();
            server.ShutdownAsync().Wait();
        }

        private static SslServerCredentials GetCredentials()
        {
            var caCert = File.ReadAllText("c:\\Certs\\ca.crt");
            var keyPair = new KeyCertificatePair(
                File.ReadAllText("c:\\Certs\\server.crt"),
                File.ReadAllText("c:\\Certs\\server.key"));
            return new SslServerCredentials(
                new List<KeyCertificatePair> { keyPair }, caCert, forceClientAuth: false);
        }
    }
}
