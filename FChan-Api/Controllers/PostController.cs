using Framework.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FChan_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IBoardService _boardService;
        private readonly IImageService _imageService;

        public PostController(ILogger<PostController> logger, IBoardService boardService, IImageService imageService)
        {
            _logger = logger;
            _boardService = boardService;
            _imageService = imageService;
        }

        [HttpGet("boards")]
        public async Task<IActionResult> GetAllBoards()
        {
            try
            {
                return Ok(await _boardService.GetAllBoards().ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting boards.",ex);
            }
            return StatusCode(500);
        }
        [HttpGet("catalog/{board}")]
        public async Task<IActionResult> GetBoardCatalog(string board)
        {
            if (string.IsNullOrEmpty(board))
                return BadRequest("GetBoardCatalog - Board value is missing or empty.");

            try
            {
                return Ok(await _boardService.GetBoardCatalog(board).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting catalog for board:{board}.", ex);
            }
            return StatusCode(500);
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetThreadPosts([FromQuery]string board, string threadNumber)
        {
            if (string.IsNullOrEmpty(board))
                return BadRequest("GetThreadPosts - Board value is missing or empty.");
            
            if(string.IsNullOrEmpty(threadNumber))
                return BadRequest("GetThreadPosts - Thread number is missing or empty.");

            try
            {
                return Ok(await _boardService.GetThreadPosts(board, threadNumber).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting posts for thread:{threadNumber} from board:{board}.", ex);
            }
            return StatusCode(500);
        }

        [HttpGet("downloadfile")]
        public async Task<IActionResult> DownloadFile([FromQuery] string fileUrl, string destination)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return BadRequest("DownloadFile - File Url is missing or empty.");

            if (string.IsNullOrEmpty(destination))
                return BadRequest("DownloadFile - Destination value is missing or empty.");

            try
            {
                return Ok(await _imageService.DownloadFile(fileUrl, destination).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error downloading file:{fileUrl} to destination:{destination}.", ex);
            }
            return StatusCode(500);
        }
    }
}