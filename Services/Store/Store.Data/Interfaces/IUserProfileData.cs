using ApiUtility.ActionFilters;
using SharedModel;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;

namespace Store.Data.Interfaces
{
    [RegisterScoped]

    //per http request 
    //every http request will get a new instance of the service
    public interface IUserProfileData
    {
        Task<UserProfileResponse> Create(UserProfileRequest request);

        Task<UserProfileResponse> GetById(int id);

        Task<UserProfileResponse> GetByUserId(int userId);

        Task<UserProfileResponse> Update(int id, UserProfileRequest request);

        Task<PagedResults<UserProfileResponse>> GetAll(
            int pageNumber = 1,
            int pageSize = 10,
            string? search =null);

    }
}
