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
    [RegisterScoped]
    public interface IOrderData
    {

        Task <OrderResponse> Create (OrderRequest request);

        Task<OrderResponse> GetById(int id);

        Task<PagedResults<OrderResponse>> GetAllByUserId(
          int userId,
          int pageNumber = 1,
          int pageSize = 10,
          string? searchTerm = null
          );


        //Get all orders for a users
        Task<PagedResults<OrderResponse>> GetAll(
            int pageNumber=1,
            int pageSize=10,
            string? searchTerm= null
            );
         Task<OrderResponse> Update(int id, OrderRequest request);

        Task<bool> Delete(int id);

    }
}
