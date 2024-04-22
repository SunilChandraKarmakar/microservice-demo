using MicroserviceDemo.DiscountApi.Models;
using MicroserviceDemo.DiscountApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MicroserviceDemo.DiscountApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;
        public DiscountController(ICouponRepository couponRepository) => _couponRepository = couponRepository;

        [HttpGet]
        [ProducesResponseType(typeof(Coupon), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscountByProductId(string  productId)
        {
            try
            {
                var getCouponByProductId = await _couponRepository.GetDiscountByProductId(productId);
                return Ok(getCouponByProductId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.Created)]
        public async Task<ActionResult<bool>> CreateDiscount([FromBody]Coupon coupon)
        {
            try
            {
                var createDiscount = await _couponRepository.CreateDiscount(coupon);
                return Ok(createDiscount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateDiscount([FromBody] Coupon coupon)
        {
            try
            {
                var updateDiscount = await _couponRepository.UpdateDiscount(coupon);
                return Ok(updateDiscount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteDiscount(string productId)
        {
            try
            {
                var deleteDiscount = await _couponRepository.DeleteDiscount(productId);
                return Ok(deleteDiscount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}