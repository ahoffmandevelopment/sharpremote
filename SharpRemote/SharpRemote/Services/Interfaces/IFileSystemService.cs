using System.IO;
using System.Threading.Tasks;

namespace SharpRemote.Services.Interfaces
{
    public interface IFileSystemService
    {
        string AppDataDirectory { get; }
        string CacheDirectory { get; }

        Task<Stream> OpenAppPackageFileAsync(string fileName);
    }
}