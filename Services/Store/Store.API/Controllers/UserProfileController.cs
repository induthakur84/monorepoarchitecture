using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Data.Interfaces;

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
    }
}
