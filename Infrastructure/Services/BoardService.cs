using Framework.DataModels;
using Framework.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BoardService : IBoardService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public BoardService(HttpClient httpClient, ILogger<ImageService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }


        public async Task<AllBoards> GetAllBoards()
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<AllBoards>
                    (await _httpClient.GetStreamAsync($"boards.json"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error getting board listing.", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Catalogue>> GetBoardCatalog(string board)
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Catalogue>>(await _httpClient.GetStreamAsync($"/{board}/catalog.json"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error getting catalog for board:{board}.", ex);
                throw;
            }
        }


        public async Task<ThreadPosts> GetThreadPosts(string board, string threadNumber)
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<ThreadPosts>(await _httpClient.GetStreamAsync($"/{board}/thread/{threadNumber}.json"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error getting posts for thread:{threadNumber} in board:{board}.", ex);
                throw;
            }
        }
    }
}
