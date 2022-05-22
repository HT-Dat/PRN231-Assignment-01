using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Repository;

namespace eStoreAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        // GET: api/OrderDetail
        [HttpGet("order-details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDetail>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
            var list = await _orderDetailRepository.GetAll();
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        // GET: api/order/6113/product/4
        [HttpGet("order-detail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDetail))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetail(int? productId, int orderId)
        {
            IEnumerable<OrderDetail> orderDetail = await _orderDetailRepository.Get(productId, orderId);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return Ok(orderDetail);
        }

        // PUT: api/OrderDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("order-detail")]
        public async Task<IActionResult> PutOrderDetail(OrderDetail orderDetail)
        {

            try
            {
                await _orderDetailRepository.Update(orderDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _orderDetailRepository.Get(orderDetail.ProductId, orderDetail.OrderId) == null)
                {
                    return NotFound();
                }

                throw;
            }


            return NoContent();
        }

        // POST: api/OrderDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("order-detail")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                await _orderDetailRepository.Add(orderDetail);
            }
            catch (DbUpdateException)
            {
                if (await _orderDetailRepository.Get(orderDetail.ProductId, orderDetail.OrderId) != null)
                {
                    return Conflict();
                }

                return NotFound();
            }

            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.OrderId }, orderDetail);
        }

        // DELETE: api/OrderDetail/5
        [HttpDelete("order-detail")]
        public async Task<IActionResult> DeleteOrderDetail(int productId, int orderId)
        {
            var orderDetail = await _orderDetailRepository.Get(productId, orderId);
            if (orderDetail == null)
            {
                return NotFound();
            }

            await _orderDetailRepository.Delete(productId, orderId);
            return NoContent();
        }
    }
}