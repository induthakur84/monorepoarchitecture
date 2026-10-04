using ApiUtility.ActionFilters;
using SharedModel;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Interfaces
{

    //abstraction



    [RegisterScoped]
    public interface IOrderData
    {
        Task<OrderResponse> CreateAsync(OrderRequest request);

        Task<OrderResponse> GetByIdAsync(int id);

        // Get all orders for a specific user
        Task<PagedResults<OrderResponse>> GetByUserIdAsync(
          int userId,
          int pageNumber = 1,
          int pageSize = 10,
            string? search = null);

        // Get all orders with Pagination + Search
        // search can match User.Name / User.Email (and you can extend later)
        Task<PagedResults<OrderResponse>> GetAllAsync(
     int pageNumber = 1,
     int pageSize = 10,
     string? search = null,
     string? sortBy = null,
     string? sortOrder = "desc");
        Task<OrderResponse> UpdateAsync(int id, OrderRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
