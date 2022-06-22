using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly ICacheService _cacheService;
        public StringTypeController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public IActionResult Index()
        {
           
            TimeSpan t = TimeSpan.FromSeconds(1000);
            _cacheService.SetCacheValueAsync("Name", "Damla", t);

            return View();
        }
        public IActionResult Show()
        {
           var name= _cacheService.GetCacheValueAsync<string>("Name");

            ViewBag.value = name;

            return View();
        }


    }
}
