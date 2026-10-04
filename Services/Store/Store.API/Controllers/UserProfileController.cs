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
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileData _userProfileData;

        public UserProfileController(IUserProfileData userProfileData)
        {
            _userProfileData = userProfileData;
        }

        // ✅ Create User Profile
        [HttpPost]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserProfileResponse>))]
        public async Task<UserProfileResponse> Create(UserProfileRequest request)
        {
            return await _userProfileData.CreateAsync(request);
        }

        // ✅ Get All User Profiles (Pagination + Search)
        [HttpGet]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserProfileResponse>))]
        public async Task<PagedResults<UserProfileResponse>> GetAll(
            int pageNumber = 1,
            int pageSize = 10,
            string? search = null)
        {
            return await _userProfileData.GetAllAsync(pageNumber, pageSize, search);
        }

        // ✅ Get Profile By Profile Id
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserProfileResponse>))]
        public async Task<UserProfileResponse> GetById(int id)
        {
            return await _userProfileData.GetByIdAsync(id);
        }

        // ✅ Get Profile By UserId (One-to-One)
        [HttpGet("by-user/{userId}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserProfileResponse>))]
        public async Task<UserProfileResponse> GetByUserId(int userId)
        {
            return await _userProfileData.GetByUserIdAsync(userId);
        }

        // ✅ Update User Profile
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserProfileResponse>))]
        public async Task<UserProfileResponse> Update(int id, UserProfileRequest request)
        {
            return await _userProfileData.UpdateAsync(id, request);
        }

        // ✅ Delete User Profile
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<bool>))]
        public async Task<bool> Delete(int id)
        {
            return await _userProfileData.DeleteAsync(id);
        }
    }
}