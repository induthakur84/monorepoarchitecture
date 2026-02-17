using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        public UserProfileData(StoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<UserProfileResponse> Create(UserProfileRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResults<UserProfileResponse>> GetAll(int pageNumber = 1,
            int pageSize = 10, 
            string? search = null)
        {

            // here we add user because we email and name from user seaching
            var query = _context.UserProfiles
                    .Include(x => x.User)
                      .AsNoTracking()
                      .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                // here we can add search with multiple fields:

                //Address
                //PhoneNumber
                //User name
                // useremail

                query = query.Where(x =>
                    x.Address.ToLower().Contains(search) ||
                     x.PhoneNumber.ToLower().Contains(search) ||
                      x.User.Name.ToLower().Contains(search) ||
                       x.User.Email.ToLower().Contains(search)
                 );
            }

                var totalCount= await query.CountAsync();

                //

                //pageNumber= 2, pageSize=10 --> it will skip (10)
                 //1-1=0
                 //2-2=1

                //Skip : ignores records from previous pages
                //Take: Limits number of record returned
                var data= await query.
                          Skip((pageNumber-1)*pageSize)
                          .Take(pageSize)
                          .Select(x=> _mapper.Map<UserProfileResponse>(x))
                          .ToListAsync();

                return new PagedResults<UserProfileResponse>
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalNumberOfRecords = totalCount,
                    Results = data
                };
            
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
