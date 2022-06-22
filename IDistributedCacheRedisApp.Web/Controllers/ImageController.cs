using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ImageController : Controller
    {

        private IDistributedCache _distributedCache;
        public ImageController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public IActionResult Index()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/cat.jpg");
            byte[] imageByte = System.IO.File.ReadAllBytes(path);
            _distributedCache.Set("image", imageByte);
            return View();
        }
        public IActionResult ImageShow()
        {
            return View();
        }

        public IActionResult ImageUrl()
        {
            byte[] imageUrl = _distributedCache.Get("image");
            return File(imageUrl, "image/jpg");
        }
    }
}
