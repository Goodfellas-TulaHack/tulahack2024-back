using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TulaHack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadPhoto()
        {
            try
            {
                var formFiles = Request.Form.Files;

                if (formFiles == null || formFiles.Count == 0)
                {
                    return BadRequest("Нет загруженных файлов.");
                }

                foreach (var formFile in formFiles)
                {
                    if (formFile.Length > 0)
                    {
                        var filePath = Path.Combine(@"Files\", formFile.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                return Ok("Файл/файлы загружены.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{imagename}")]
        public IActionResult Get(string imagename)
        {
            try
            {
                var b = System.IO.File.ReadAllBytes(@"Files\" + imagename);
                return File(b, "image/jpeg");
            }
            catch
            {
                return NotFound("Image not found");
            }
        }
    }
}
