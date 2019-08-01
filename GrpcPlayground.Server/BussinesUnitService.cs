namespace GrpcPlayground.Server
{
    using System.Threading.Tasks;
    using System.Linq;
    using Grpc.Core;
    using Messages;

    public class BussinesUnitService : BusinessUnitService.BusinessUnitServiceBase
    {
        public override async Task GetAll(GetAllRequest request,
            IServerStreamWriter<GetAllResponse> responseStream,
            ServerCallContext context)
        {
            foreach (var index in Enumerable.Range(0, 150))
            {
                await Task.Delay(30);
                await responseStream.WriteAsync(
                    new GetAllResponse
                        {BusinessUnit = new BusinessUnit {UnitLName = $"Unit{index}", Wbs = $"Wbs{index}"}});
            }
        }
    }
}
