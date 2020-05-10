using Framework.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public ImageService(HttpClient httpClient, ILogger<ImageService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> DownloadFile(string fileUrl, string destination)
        {
            try
            {
                var directoryName = Path.GetDirectoryName(destination);
                if (!string.IsNullOrEmpty(directoryName))
                {
                    if (!Directory.Exists(directoryName))
                    {
                        System.IO.Directory.CreateDirectory(directoryName);
                    }
                }

                using var result = await _httpClient.GetAsync(fileUrl).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
                    await File.WriteAllBytesAsync(destination,
                            await result.Content.ReadAsByteArrayAsync().ConfigureAwait(false))
                        .ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error downloading file: {fileUrl}",ex);
                throw;
            }
            return "done";
        }
    }
}