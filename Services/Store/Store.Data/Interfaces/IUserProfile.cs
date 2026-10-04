using ApiUtility.ActionFilters;
using SharedModel;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;

namespace Store.Data.Interfaces
{
    [RegisterScoped]
    public interface IUserProfileData
    {
        // Create Profile (usually requires UserId in request)
        Task<UserProfileResponse> CreateAsync(UserProfileRequest request);

        // Get Profile by ProfileId
        Task<UserProfileResponse> GetByIdAsync(int id);

        // Get Profile by UserId (common in 1-to-1)
        Task<UserProfileResponse> GetByUserIdAsync(int userId);

        // Get All with Pagination + Search
        // search can match Address / PhoneNumber / User.Name / User.Email etc.
        Task<PagedResults<UserProfileResponse>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? search = null);

        // Update Profile
        Task<UserProfileResponse> UpdateAsync(int id, UserProfileRequest request);

        // Delete Profile
        Task<bool> DeleteAsync(int id);
    }
}