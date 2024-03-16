using MicroserviceDemo.CatalogApi.Interfaces.Manager;
using MicroserviceDemo.CatalogApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroserviceDemo.CatalogApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;
        public ProductController(IProductManager productManager) => _productManager = productManager;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 10)]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            try
            {
                var products = await Task.Run(() => _productManager.GetAll());
                return Ok(products);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}