using SharpRemote.Services;
using SharpRemote.Services.Interfaces;
using Sockets.Plugin;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(TcpService))]
namespace SharpRemote.Services
{
    /// <summary>
    /// API for interacting with TCP service.
    /// </summary>
    public class TcpService : ITcpService
    {
        private readonly TcpSocketClient client;

        public TcpService()
        {
            client = new TcpSocketClient();
        }

        /// <summary>
        /// Establishes a TCP connection to an endpoint.
        /// </summary>
        /// <returns>A task that represents the asynchronous connect operation.</returns>
        public async Task ConnectToClientAsync()
        {
            // TODO: Allow dynamic entry.
            const string ADDRESS = "192.168.1.123";
            const int PORT = 10002;

            await client.ConnectAsync(ADDRESS, PORT);
        }

        /// <summary>
        /// Disconnects from and enpoint previously connected to using <see cref="ConnectToClientAsync"/>.
        /// </summary>
        /// <returns>A task that represents the asynchronous disconnect operation.</returns>
        public async Task DisconnectFromClientAsync()
        {
            if (client.Socket.Connected)
            {
                await client.DisconnectAsync();
            }
        }

        /// <summary>
        /// Refreshes the client connection by disconnecting then reconnecting to the client.
        /// </summary>
        /// <returns>A task that represents the asynchronous refresh operation.</returns>
        public async Task RefreshConnectionAsync()
        {
            await DisconnectFromClientAsync();

            await ConnectToClientAsync();
        }

        /// <summary>
        /// Asynchronously writes a string to the current stream and advances
        /// the current position within this stream by the number of bytes written. 
        /// </summary>
        /// <param name="data">The string to write to the stream.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        /// <exception cref="System.ArgumentNullException">Buffer is null.</exception> 
        /// <exception cref="System.ArgumentOutOfRangeException">Offset or count is negative.</exception>
        /// <exception cref="System.ArgumentException">The sum of offset and count is larger than the buffer length.</exception>
        /// <exception cref="System.NotSupportedException">The stream does not support writing.</exception>
        /// <exception cref="System.ObjectDisposedException">The stream has been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">The stream is currently in use by a previous write operation.</exception>
        public async Task WriteAsync(string data)
        {
            var bytes = Encoding.ASCII.GetBytes(data);

            await WriteAsync(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Asynchronously writes a sequence of bytes to the current stream and advances
        /// the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">The zero-based byte offset in buffer from which to begin copying bytes to the stream.</param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        /// <exception cref="System.ArgumentNullException">Buffer is null.</exception> 
        /// <exception cref="System.ArgumentOutOfRangeException">Offset or count is negative.</exception>
        /// <exception cref="System.ArgumentException">The sum of offset and count is larger than the buffer length.</exception>
        /// <exception cref="System.NotSupportedException">The stream does not support writing.</exception>
        /// <exception cref="System.ObjectDisposedException">The stream has been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">The stream is currently in use by a previous write operation.</exception>
        public async Task WriteAsync(byte[] buffer, int offset, int count)
        {
            await client.WriteStream.WriteAsync(buffer, offset, count);
            await client.WriteStream.FlushAsync();            
        }

        private async Task<string> ReadAsync()
        {
            byte[] resultBuffer = new byte[4];

            await client.ReadStream.ReadAsync(resultBuffer, 0, 4);

            var resultString = Encoding.ASCII.GetString(resultBuffer);

            Debug.WriteLine($"----------- {resultString}");

            return resultString;
        }
    }
}
