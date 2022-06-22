using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private IDistributedCache _distributedCache;
        public ProductController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<IActionResult> IndexAsync()
        {
            DistributedCacheEntryOptions options= new DistributedCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            _distributedCache.SetString("a","b",options);
            
            Product p1= new Product() { Id = 1 , Name="Product1"};
            Product p2= new Product() { Id = 2 , Name="Product2"};
            var jsonProduct = JsonConvert.SerializeObject(p1);
            var jsonProduct1 = JsonConvert.SerializeObject(p2);
            await _distributedCache.SetStringAsync("product:1", jsonProduct);
            await _distributedCache.SetStringAsync("product:2", jsonProduct1);
           
            return View();
        }
      
    }
}
