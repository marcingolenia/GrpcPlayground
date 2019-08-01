namespace GrpcPlayground.Server
{
    using System;
    using Grpc.Core;
    class Program
    {
        static void Main(string[] args)
        {
            const int Port = 50050;
            const string host = "127.0.0.1";
            var server = new Server
            {
                Ports = { new ServerPort(host, Port, ServerCredentials.Insecure)},
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
    }
}
