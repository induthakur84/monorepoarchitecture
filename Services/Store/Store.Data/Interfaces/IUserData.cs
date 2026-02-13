using ApiUtility.ActionFilters;
using SharedModel;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;

namespace Store.Data.Interfaces
{
  
    [RegisterScoped]
 //IUserProfile
    public interface IUserData
    {
        Task<UserResponse> CreateAsync(UserRequest request);

        Task<UserResponse> GetByIdAsync(int id);

        Task<PagedResults<UserResponse>> GetAllAsync();

        Task<UserResponse> UpdateAsync(int id, UserRequest request);

        Task<bool> DeleteAsync(int id);
    }

}


    //10interface

///PageRes
//Page 1000{
