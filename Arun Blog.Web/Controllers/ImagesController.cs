using ArunBlog.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ArunBlog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ImageRepository imageRepository;

        public ImagesController(ImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
           //call the repository 
           var imageURL = await imageRepository.UploadAsync(file);
           
            if (imageURL == null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { Link = imageURL });
        }
    }
}
