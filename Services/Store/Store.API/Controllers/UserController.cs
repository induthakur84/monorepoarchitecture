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
        private readonly IUserData _userData;
        // 1 same object created for userprofile and usercontroller and reused in both controllers
        //if i want used here addscoped then it will reuse the object that we created in userprofile



        //2 new object
        // if i want  used here addtransient then it will create new object for userprofile and usercontroller
       // private readonly IUserProfileData _userProfileData;


        public UserController(IUserData userData)
        {
            _userData = userData;
        }
        [HttpPost]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserResponse>))]
        public async Task<UserResponse> Create([FromBody] UserRequest request)
        {
            return await _userData.CreateAsync(request);
        }
        [HttpGet]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserResponse>))]
        public async Task<PagedResults<UserResponse>> GetAll()
        {
            return await _userData.GetAllAsync();
        }
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserResponse>))]
        public async Task<UserResponse> GetById(int id)
        {
            return await _userData.GetByIdAsync(id);
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ResponseFilterAttribute<UserResponse>))]
        public async Task<UserResponse> Update(int id, [FromBody] Domain.DTO.Request.UserRequest request)
        {
            return await _userData.UpdateAsync(id, request);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userData.DeleteAsync(id);
            if (result)
                return NoContent();
            else
                return NotFound();
        }
    }
}
