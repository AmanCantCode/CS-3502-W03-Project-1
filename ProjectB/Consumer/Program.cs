using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

 class Program
 {
     static async Task Main(string[] args)
     {
         using (var pipe = new NamedPipeClientStream(".", "testpipe", PipeDirection.In))
         {
             Console.WriteLine("Consumer: Connecting to pipe...");
             pipe.Connect();
             Console.WriteLine("Consumer: Connected.");

             byte[] buffer = new byte[256];
             int bytesRead = await pipe.ReadAsync(buffer, 0, buffer.Length);

             string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
             Console.WriteLine($"Consumer: Message received: {message}");
         }
     }
 }


