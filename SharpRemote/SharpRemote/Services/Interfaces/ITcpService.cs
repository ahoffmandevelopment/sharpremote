using System.Threading.Tasks;

namespace SharpRemote.Services.Interfaces
{
    public interface ITcpService
    {
        Task ConnectToClientAsync();
        Task DisconnectFromClientAsync();
        Task RefreshConnectionAsync();
        Task WriteAsync(byte[] buffer, int offset, int count);
        Task WriteAsync(string data);
    }
}