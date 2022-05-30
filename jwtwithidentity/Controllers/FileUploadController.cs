using jwtwithidentity.Model;
using jwtwithidentity.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace jwtwithidentity.Controllers
{
    [Route("api/FileUpload")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;

        public FileUploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles([FromForm] List<IFormFile> files)
        {
            //var uploadResponse = await _uploadService.UploadFiles(files);
            var uploadResponse = await _uploadService.UploadFiles(files);
            if (uploadResponse.ErrorMessage != "")
                return BadRequest(new { error = uploadResponse.ErrorMessage });
            return Ok(uploadResponse);
        }

        [Route("DownloadFile")]
        [HttpGet]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var stream = await _uploadService.DownloadFile(id);
            if (stream == null)
                return NotFound();
            return new FileContentResult(stream, "application/octet-stream");
        }

        [Route("DownloadFiles")]
        [HttpGet]
        public async Task<List<FileDownloadView>> DownloadFiles()
        {
            var files = await _uploadService.DownloadFiles();
            return files.ToList();
        }


        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
               
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                string name = file.FileName.Replace(@"\\\\", @"\\");
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = Path.GetFileName(name);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }


        }
    }
}
