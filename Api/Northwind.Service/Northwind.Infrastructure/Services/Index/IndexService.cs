using Grpc.Net.Client;
using Northwind.Application.Services.Index;
using Index.API.Protos;

namespace Northwind.Infrastructure.Services.Index
{
    public class IndexService : IIndexService
    {
        public IndexService()
        {

        }

        public async Task<IEnumerable<string>> SearchKeyword(string index, string keyword, int limit)
        {
            var data = new HelloRequest { Name = "Joydip" };
            var grpcChannel = GrpcChannel.ForAddress("https://localhost:5002");
            var client = new Greeter.GreeterClient(grpcChannel);
            try
            {
                var response = await client.SayHelloAsync(data);
                Console.WriteLine(response.Message);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            Console.ReadLine();
            return new string[] { "1", "2", "3" };
        }
    }
}
