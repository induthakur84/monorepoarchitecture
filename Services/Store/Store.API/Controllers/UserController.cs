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
    public class UserController : ControllerBase
    {

        // here we are able to implement abstraction with the help of
        // interface because  we can hide the
        // implementation call only the method names
        private readonly IUserData _userData;

        public UserController(IUserData userData)
        {
            _userData = userData;
        }

        // ✅ Create User
        [HttpPost]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserResponse>))]
        public async Task<UserResponse> Create(UserRequest request)
        {
            return await _userData.CreateAsync(request);
        }

        // ✅ Get All Users
        [HttpGet]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserResponse>))]
        public async Task<PagedResults<UserResponse>> GetAll()
        {
            return await _userData.GetAllAsync();
        }
        // ✅ Get User By Id
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserResponse>))]
        public async Task<UserResponse> GetById(int id)
        {
            return await _userData.GetByIdAsync(id);
        }

        // ✅ Update User
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserResponse>))]
        public async Task<UserResponse> Update(int id, UserRequest request)
        {
            return await _userData.UpdateAsync(id, request);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<bool>))]
        public async Task<bool> Delete(int id)
        {
            return await _userData.DeleteAsync(id);
        }
    }
}