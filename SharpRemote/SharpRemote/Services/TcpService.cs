using SharpRemote.Services;
using SharpRemote.Services.Interfaces;
using Sockets.Plugin;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(TcpService))]
namespace SharpRemote.Services
{
    public class TcpService : ITcpService
    {
        private readonly TcpSocketClient client;

        public TcpService()
        {
            client = new TcpSocketClient();
        }

        public async Task ConnectToClientAsync()
        {
            // TODO: Allow dynamic entry.
            const string ADDRESS = "192.168.1.123";
            const int PORT = 10002;

            await client.ConnectAsync(ADDRESS, PORT);
        }

        public async Task DisconnecFromClientAsync()
        {
            if (client.Socket.Connected)
            {
                await client.DisconnectAsync();
            }
        }

        public async Task WriteAsync(string data)
        {
            var bytes = Encoding.ASCII.GetBytes(data);

            await WriteAsync(bytes, 0, bytes.Length);
        }

        public async Task WriteAsync(byte[] buffer, int offset, int count)
        {
            await client.WriteStream.WriteAsync(buffer, offset, count);
            await client.WriteStream.FlushAsync();
        }
    }
}
