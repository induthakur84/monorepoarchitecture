using SharedModel;
using Store.Data.Context;
using Store.Data.Interfaces;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;

namespace Store.Data
{
    public class UserProfileData : IUserProfileData
    {
        private readonly StoreDbContext _context;

        public UserProfileData(StoreDbContext context)
        {
            _context = context;
        }
        public Task<UserProfileResponse> Create(UserProfileRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResults<UserProfileResponse>> GetAll(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
           
        }

        public Task<UserProfileResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileResponse> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileResponse> Update(int id, UserProfileRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
