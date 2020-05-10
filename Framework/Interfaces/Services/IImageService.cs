using System;
using System.Threading.Tasks;

namespace Framework.Interfaces.Services
{
    public interface IImageService
    {
        Task<string> DownloadFile(string fileUrl, string destination);
    }
}
