using SharpRemote.Services;
using SharpRemote.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystemService))]
namespace SharpRemote.Services
{
    public class FileSystemService : IFileSystemService
    {
        public string AppDataDirectory => FileSystem.AppDataDirectory;

        public string CacheDirectory => FileSystem.CacheDirectory;

        public async Task<Stream> OpenAppPackageFileAsync(string fileName) => await FileSystem.OpenAppPackageFileAsync(fileName);
    }
}
