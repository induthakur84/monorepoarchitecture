using ApiUtility.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModel;
using Store.Data;
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

        // create new user profile
        [HttpPost]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserProfileResponse>))]
        public async Task<UserProfileResponse> Create([FromBody] UserProfileRequest request)
        {
            return await _userProfileData.Create(request);
        }


        //Get all user profiles with pagination and search
        [HttpGet]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserProfileResponse>))]
        public async Task<PagedResults<UserProfileResponse>> GetAll(
            int pageNumber = 1,
            int pageSize = 10,
            string? search = null)
        {
            return await _userProfileData.GetAll(pageNumber, pageSize, search);
        }



        // get user profile by id
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserProfileResponse>))]
        public async Task<UserProfileResponse> GetById(int id)
        {
            return await _userProfileData.GetById(id);
        }

        //get user profile by user id
        [HttpGet("{userId}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserProfileResponse>))]
        public async Task<UserProfileResponse> GetByUserId(int userId)
        {
            return await _userProfileData.GetByUserId(userId);

        }

        // update user profile
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserProfileResponse>))]
        public async Task<UserProfileResponse> Update(int id, [FromBody] UserProfileRequest request)
        {
            return await _userProfileData.Update(id, request);
        }

        // delete user profile
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<bool>))]
        public async Task<bool> Delete(int id)
        {
            return await _userProfileData.Delete(id);

        }
    }

}
