using System;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyIpcTests
{
    public class IpcCommunicationTests
    {
        [Fact]
        public async Task ProducerConsumerCommunicationTest()
        {
            var producerTask = Task.Run(async () =>
            {
                using (var pipe = new NamedPipeServerStream("testpipe", PipeDirection.Out))
                {
                    pipe.WaitForConnection();
                    string message = "Hello from Producer!";
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    await pipe.WriteAsync(messageBytes, 0, messageBytes.Length);
                }
            });

            var consumerTask = Task.Run(async () =>
            {
                using (var pipe = new NamedPipeClientStream(".", "testpipe", PipeDirection.In))
                {
                    pipe.Connect();
                    byte[] buffer = new byte[256];
                    int bytesRead = await pipe.ReadAsync(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Assert.Equal("Hello from Producer!", message);
                }
            });

            await Task.WhenAll(producerTask, consumerTask);
        }
    }
}
