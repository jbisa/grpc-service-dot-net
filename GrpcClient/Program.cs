using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // A channel represents a connection from the client to the server
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Greeter.GreeterClient(channel);

            /*
            var response = await client.SayHelloAsync(new HelloRequest
            {
                Name = "Niam Bisa"
            });

            Console.WriteLine($"From server: {response.Message}");*/

            var call = client.SayHelloStream(new HelloRequest
            {
                Name = "Niam Bisa"
            });

            await foreach (var item in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"From server: {item.Message}");
            }
        }
    }
}
