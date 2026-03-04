using ApiUtility.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using SharedModel;
using Store.Data.Interfaces;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;

namespace Store.API.Controllers
{

    // Unit testing
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderData _orderData;
        public OrderController(IOrderData orderData)
        {
            _orderData = orderData;
        }


        //api/order/Create
        [HttpPost]
        [ServiceFilter(typeof(ResponseFilterAttribute<OrderResponse>))]
        public async Task<OrderResponse> Create(OrderRequest request)
        {
            return await _orderData.Create(request);
        }

        //Get All Orders(Pagination + Search)
        //api/order?pageNumber=1&pageSize=10&searchTerm=abc
        [HttpGet]
        [ServiceFilter(typeof(ResponseFilterAttribute<OrderResponse>))]
        public async Task<PagedResults<OrderResponse>> GetAll(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null
            )
        {
            return await _orderData.GetAll(pageNumber, pageSize, searchTerm);
        }

        //Get Order By Id
        //api/order/1
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<OrderResponse>))]
        public async Task<OrderResponse> GetById(int id)
        {
            return await _orderData.GetById(id);
        }

        //Get All Orders for a User (Pagination + Search)

        //api/User/ username/ram/Pasword/abc



        //api/order/user/1?pageNumber=1&pageSize=10&searchTerm=abc
        [HttpGet("User/{userId}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<OrderResponse>))]
        public async Task<PagedResults<OrderResponse>> GetAllByUserId(
            int userId,
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            return await _orderData.GetAllByUserId(userId, pageNumber, pageSize, searchTerm);
        }

        //Update Order
        //api/order/1
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<OrderResponse>))]
        public async Task<OrderResponse> Update(int id, OrderRequest request)
        {
            return await _orderData.Update(id, request);
        }

        //Delete Order
        //api/order/1
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<bool>))]
        public async Task<bool> Delete(int id)
        {
            return await _orderData.Delete(id);

        }
    }
}
