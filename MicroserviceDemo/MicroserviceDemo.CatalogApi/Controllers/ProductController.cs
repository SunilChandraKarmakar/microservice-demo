using MicroserviceDemo.CatalogApi.Interfaces.Manager;
using MicroserviceDemo.CatalogApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                    return BadRequest($"Product id cannot found! Error:- {HttpStatusCode.BadRequest}");

                var existProduct = await Task.Run(() => _productManager.GetById(id));

                if (existProduct == null)
                    return NotFound($"Product cannot found! Error:- {HttpStatusCode.NotFound}");

                return Ok(existProduct);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            try
            {
                // Set unique product id
                product.Id = ObjectId.Parse(product.Id).ToString();

                var isProductSaved = await Task.Run(() => _productManager.Add(product));

                if (isProductSaved)
                    return Ok(product);

                return BadRequest($"Product cannot saved! Error:- {HttpStatusCode.BadRequest}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> Update([FromBody] Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.Id) || string.IsNullOrWhiteSpace(product.Id))
                    return BadRequest($"Product id cannot found! Error:- {HttpStatusCode.BadRequest}");

                var isUpdate = await Task.Run(() => _productManager.Update(product.Id, product));

                if (isUpdate)
                    return Ok(product);

                return BadRequest($"Product cannot updated! {HttpStatusCode.BadRequest}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                    return BadRequest($"Product id cannot found! Error:- {HttpStatusCode.BadRequest}");

                var isDeleted = await Task.Run(() => _productManager.Delete(id));

                if (isDeleted)
                    return Ok(id);

                return BadRequest($"Product cannot deleted! {HttpStatusCode.BadRequest}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}