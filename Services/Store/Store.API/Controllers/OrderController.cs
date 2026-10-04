using ApiUtility.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using SharedModel;
using Store.Data.Interfaces;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderData _orderData;

        public OrderController(IOrderData orderData)
        {
            _orderData = orderData;
        }

        // ✅ Create Order
        [HttpPost]
        [ServiceFilter(typeof(ResponseFilterAttribute<OrderResponse>))]
        public async Task<OrderResponse> Create(OrderRequest request)
        {
            return await _orderData.CreateAsync(request);
        }

        // ✅ Get All Orders (Pagination + Search)
        [HttpGet]
        [ServiceFilter(typeof(ResponseFilterAttribute<OrderResponse>))]
        public async Task<PagedResults<OrderResponse>> GetAll(
       int pageNumber = 1,
       int pageSize = 10,
       string? search = null,
       string? sortBy = null,
       string? sortOrder = "desc")
        {
            return await _orderData.GetAllAsync(
                pageNumber,
                pageSize,
                search,
                sortBy,
                sortOrder);
        }
        // ✅ Get Order By Id
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<OrderResponse>))]
        public async Task<OrderResponse> GetById(int id)
        {
            return await _orderData.GetByIdAsync(id);
        }

        // ✅ Get Orders By UserId
        [HttpGet("by-user/{userId}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<OrderResponse>))]
        public async Task<PagedResults<OrderResponse>> GetByUserId(
            int userId,
            int pageNumber = 1,
            int pageSize = 10,
            string? search = null)
        {
            return await _orderData.GetByUserIdAsync(userId, pageNumber, pageSize, search);
        }

        // ✅ Update Order
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<OrderResponse>))]
        public async Task<OrderResponse> Update(int id, OrderRequest request)
        {
            return await _orderData.UpdateAsync(id, request);
        }

        // ✅ Delete Order
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<bool>))]
        public async Task<bool> Delete(int id)
        {
            return await _orderData.DeleteAsync(id);
        }
    }
}