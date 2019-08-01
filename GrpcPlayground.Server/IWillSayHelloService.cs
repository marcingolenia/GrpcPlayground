using System.Threading.Tasks;
using Grpc.Core;
using Messages;

namespace GrpcPlayground.Server
{
    public class IWillSayHelloService : Messages.IWillSayHelloService.IWillSayHelloServiceBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context) =>
            Task.FromResult(new HelloReply
                {
                    Message = $"Oh! Hello {request.Name}"
                });
    }
}