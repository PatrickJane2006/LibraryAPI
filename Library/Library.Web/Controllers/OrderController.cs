﻿using Library.Application.DTO; 
using Library.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize] 
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            var orderId = await _orderService.CreateOrderAsync(userId, dto);
            return Ok(new { OrderId = orderId });
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyOrders()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            var orders = await _orderService.GetUserOrdersAsync(userId);
            return Ok(orders);
        }

        // Отменить заказ (админ или владелец)
        [HttpPost("{orderId}/cancel")]
        [Authorize]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            var roleClaim = User.FindFirst(ClaimTypes.Role);
            bool isAdmin = roleClaim != null && roleClaim.Value == "Admin";

            try
            {
                await _orderService.CancelOrderAsync(orderId, userId, isAdmin);
                return Ok(new { Message = "Заказ отменён" });
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }
    }
}
