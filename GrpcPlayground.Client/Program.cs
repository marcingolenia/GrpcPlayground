using System.Collections.Generic;
using System.IO;

namespace GrpcPlayground.Client
{
    using System;
    using System.Threading.Tasks;
    using Grpc.Core;
    using Messages;
    using System.Threading;
    using System.Linq;

    class Program
    {
        static async Task Main(string[] args)
        {
            const int port = 50050;
            const string host = "DESKTOP-DERTRE3";
            var channel = new Channel(host, port, GetCredentials());
            var client = new BusinessUnitService.BusinessUnitServiceClient(channel);
            Console.WriteLine("GrpcClient is ready to issue requests. Press any key to start");
            Console.ReadKey();
            var _ = SayHello(channel);
            using (var call = client.GetAll(new GetAllRequest()))
            {
                var responseStream = call.ResponseStream;
                while (await responseStream.MoveNext(CancellationToken.None))
                {
                    Console.WriteLine(responseStream.Current.BusinessUnit.UnitLName);
                }
            }
            Console.WriteLine("\n\nFinished GetAll request with server-stream demo. Press any key to close this window");
            Console.ReadKey();
        }

        public static async Task SayHello(Channel channel)
        {
            var client = new IWillSayHelloService.IWillSayHelloServiceClient(channel);
            foreach (var index in Enumerable.Range(0, 50))
            {
                await Task.Delay(30);
                var greetingsReply = await client.SayHelloAsync(new HelloRequest {Name = $"Marcin{index}"});
                Console.WriteLine(greetingsReply.Message);
            }
        }

        private static SslCredentials GetCredentials() =>
            new SslCredentials(
                    File.ReadAllText("c:\\certs\\ca.crt"),
                    new KeyCertificatePair(
                        File.ReadAllText("c:\\certs\\client.crt"),
                        File.ReadAllText("c:\\certs\\client.key")));
    }
}
