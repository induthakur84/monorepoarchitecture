using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedModel;
using Store.Data.Context;
using Store.Data.Interfaces;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;
using Store.Domain.Entities;

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
        public async Task<UserProfileResponse> Create(UserProfileRequest request)
        {
            var entity = _mapper.Map<UserProfile>(request);
            await _context.UserProfiles.AddAsync(entity);
            await _context.SaveChangesAsync();

            var result = await _context.UserProfiles
                   .AsNoTracking()
                   .Include(x => x.User)
                     .FirstOrDefaultAsync(x => x.Id == entity.Id);

            return _mapper.Map<UserProfileResponse>(result);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.UserProfiles.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _context.UserProfiles.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
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

            var totalCount = await query.CountAsync();

            //

            //pageNumber= 2, pageSize=10 --> it will skip (10)
            //1-1=0
            //2-2=1

            //Skip : ignores records from previous pages
            //Take: Limits number of record returned
            var data = await query.
                      Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize)
                      .Select(x => _mapper.Map<UserProfileResponse>(x))
                      .ToListAsync();

            return new PagedResults<UserProfileResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalNumberOfRecords = totalCount,
                Results = data
            };
        }


        // what is eager loading and lazy loading ?
        public async Task<UserProfileResponse> GetById(int id)
        {
            var entity = _context.UserProfiles
                   .AsNoTracking()
                   .Include(x => x.User)
                     .FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }
            return _mapper.Map<UserProfileResponse>(entity);
        }

        public async Task<UserProfileResponse> GetByUserId(int userId)
        {
            var entity = _context.UserProfiles
                   .AsNoTracking()
                   .Include(x => x.User)
                     .FirstOrDefault(x => x.UserId == userId);
            if (entity == null)
            {
                return null;
            }
            return _mapper.Map<UserProfileResponse>(entity);
        }

        public async Task<UserProfileResponse> Update(int id, UserProfileRequest request)
        {
            var entity = _context.UserProfiles
                .Include(x => x.User)
                .FirstOrDefault(x=>x.Id==id);
            if (entity == null)
            {
                return null;
            }
            _mapper.Map(request, entity);   
            await _context.SaveChangesAsync();
            return _mapper.Map<UserProfileResponse>(entity);
        }
    }
}

// why we use include keyword in get by id and get by user id ?