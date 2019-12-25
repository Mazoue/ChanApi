using Framework.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FChan_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IBoardService _boardService;
        private readonly IImageService _imageService;

        public PostController(IBoardService boardService, IImageService imageService)
        {
            _boardService = boardService;
            _imageService = imageService;
        }

        [HttpGet("boards")]
        public async Task<IActionResult> GetAllBoards()
        {
            return Ok(await _boardService.GetAllBoards().ConfigureAwait(false));
        }
        [HttpGet("catalog/{board}")]
        public async Task<IActionResult> GetBoardCatalog(string board)
        {
            return Ok(await _boardService.GetBoardCatalog(board).ConfigureAwait(false));
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetThreadPosts([FromQuery]string board, string threadNumber)
        {
            return Ok(await _boardService.GetThreadPosts(board, threadNumber).ConfigureAwait(false));
        }

        [HttpGet("downloadfile")]
        public async Task<IActionResult> DownloadFile([FromQuery] string fileUrl, string destination)
        {
            return Ok(await _imageService.DownloadFile(fileUrl, destination).ConfigureAwait(false));
        }
    }
}