using System.Threading.Tasks;

namespace SharpRemote.Services.Interfaces
{
    /// <summary>
    /// API for interacting with TCP service.
    /// </summary>
    public interface ITcpService
    {
        /// <summary>
        /// Establishes a TCP connection to an endpoint.
        /// </summary>
        /// <returns>A task that represents the asynchronous connect operation.</returns>
        Task ConnectToClientAsync();

        /// <summary>
        /// Disconnects from and enpoint previously connected to using <see cref="ConnectToClientAsync"/>.
        /// </summary>
        /// <returns>A task that represents the asynchronous disconnect operation.</returns>
        Task DisconnectFromClientAsync();

        /// <summary>
        /// Refreshes the client connection by disconnecting then reconnecting to the client.
        /// </summary>
        /// <returns>A task that represents the asynchronous refresh operation.</returns>
        Task RefreshConnectionAsync();

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
        Task WriteAsync(string data);
    }
}