using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

 class Program
 {
     static async Task Main(string[] args)
     {
         using (var pipe = new NamedPipeServerStream("testpipe", PipeDirection.Out))
         {
             Console.WriteLine("Producer: Waiting for connection...");
             pipe.WaitForConnection();
             Console.WriteLine("Producer: Connected.");

             string message = "Hello from Producer!";
             byte[] messageBytes = Encoding.UTF8.GetBytes(message);

             await pipe.WriteAsync(messageBytes, 0, messageBytes.Length);
             Console.WriteLine("Producer: Message sent.");
         }
     }
 }


