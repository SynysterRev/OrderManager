using Microsoft.AspNetCore.Mvc;
using OrderManager.Application.DTO;
using OrderManager.Application.Interfaces;
using OrderManager.Application.Services;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Repositories;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderManager.WebAPI.Controllers
{
    [Route("api/orders/{orderID}/items")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemsService _orderItemsService;

        public OrderItemsController(IOrderItemsService orderItemsService)
        {
            _orderItemsService = orderItemsService;
        }

        // GET: api/orders/{orderID}/items
        [HttpGet]
        public async Task<ActionResult<List<OrderItemResponse>>> GetOrderItemsForOrder(Guid orderID)
        {
            var orderItems = await _orderItemsService.GetAllOrderItemsByOrderID(orderID);
            return Ok(orderItems);
        }

        [HttpGet("/api/items/")]
        public async Task<ActionResult<OrderItemResponse>> GetAllOrderItems()
        {
            var orderItems = await _orderItemsService.GetAllOrderItems();
            return Ok(orderItems);
        }

        // GET api/orders/{orderID}/items/5
        [HttpGet("{orderItemID}")]
        public async Task<ActionResult<OrderItemResponse>> Get(Guid orderItemID)
        {
            var orderItem = await _orderItemsService.GetOrderItemByGuid(orderItemID);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        // POST api/orders/{orderID}/items
        [HttpPost]
        public async Task<ActionResult> Post(Guid orderID, [FromBody] OrderItemAddRequest orderItemAddRequest)
        {
            if (orderItemAddRequest == null)
            {
                return BadRequest();
            }
            orderItemAddRequest.OrderID = orderID;
            var orderItemResponse = await _orderItemsService.AddOrderItem(orderItemAddRequest);
            return CreatedAtAction(nameof(Get), new { orderID = orderID, orderItemID = orderItemResponse.OrderItemID }, orderItemResponse);
        }

        // PUT api/orders/{orderID}/items/5
        [HttpPut("{orderItemID}")]
        public async Task<ActionResult> Put(Guid orderItemID, [FromBody] OrderItemUpdateRequest orderItemUpdate)
        {
            if (orderItemUpdate == null)
            {
                return BadRequest();
            }
            orderItemUpdate.OrderItemID = orderItemID;
            var orderItemResponse = await _orderItemsService.UpdateOrderItem(orderItemUpdate);
            return Ok(orderItemResponse);
        }

        // PATCH api/orders/{orderID}/items/5
        [HttpPatch("{orderItemID}")]
        public async Task<ActionResult> Patch(Guid orderItemID, [FromBody] OrderItemPartialUpdateRequest orderItemPartialUpdate)
        {
            if (orderItemPartialUpdate == null)
            {
                return BadRequest();
            }

            orderItemPartialUpdate.OrderItemID = orderItemID;
            var orderItemResponse = await _orderItemsService.PartiallyUpdateOrderItem(orderItemPartialUpdate);
            return Ok(orderItemResponse);
        }

        // DELETE api/orders/{orderID}/items/5
        [HttpDelete("{orderItemID}")]
        public async Task<ActionResult> Delete(Guid orderItemID)
        {
            var isDeleted = await _orderItemsService.DeleteOrderItem(orderItemID);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
