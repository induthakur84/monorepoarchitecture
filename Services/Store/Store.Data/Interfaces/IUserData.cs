using ApiUtility.ActionFilters;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;

namespace Store.Data.Interfaces
{
    [RegisterScoped]
    public interface IUserData
    {
        Task<UserResponse> CreateAsync(UserRequest request);

            Task<UserResponse> GetByIdAsync(int id);
    
            Task<IEnumerable<UserResponse>> GetAllAsync();
    
            Task<UserResponse> UpdateAsync(int id, UserRequest request);
    
            Task<bool> DeleteAsync(int id);
    }
}

///PageRes
//Page 1000{
