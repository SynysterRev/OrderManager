using Microsoft.AspNetCore.Mvc;
using OrderManager.Application.DTO;
using OrderManager.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderManager.WebAPI.Controllers
{
    public class OrdersController : CustomControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> Get()
        {
            var orders = await _ordersService.GetAllOrders();
            return Ok(orders);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{orderID}")]
        public async Task<ActionResult<OrderResponse>> Get(Guid orderID)
        {
            OrderResponse? order = await _ordersService.GetOrderByGuid(orderID);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderAddRequest orderAddRequest)
        {
            if (orderAddRequest == null)
            {
                return BadRequest();
            }

            OrderResponse orderResponse = await _ordersService.AddOrder(orderAddRequest);
            return CreatedAtAction(nameof(Get), new { guid = orderResponse.OrderID }, orderResponse);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{orderID}")]
        public async Task<ActionResult> Put(Guid orderID, [FromBody] OrderUpdateRequest orderUpdateRequest)
        {
            if (orderUpdateRequest == null)
            {
                return BadRequest();
            }
            orderUpdateRequest.OrderID = orderID;
            OrderResponse orderResponse = await _ordersService.UpdateOrder(orderUpdateRequest);
            return Ok(orderResponse);
        }

        // PUT api/<OrdersController>/5
        [HttpPatch("{orderID}")]
        public async Task<ActionResult> Patch(Guid orderID, [FromBody] OrderPartialUpdateRequest orderUpdateRequest)
        {
            if (orderUpdateRequest == null)
            {
                return BadRequest();
            }
            orderUpdateRequest.OrderID = orderID;
            OrderResponse orderResponse = await _ordersService.PartiallyUpdateOrder(orderUpdateRequest);
            return Ok(orderResponse);
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{orderID}")]
        public async Task<ActionResult> Delete(Guid orderID)
        {
            await _ordersService.DeleteOrder(orderID);
            return NoContent();
        }
    }
}
