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
        private readonly StoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserProfileData(StoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // ✅ Create
        public async Task<UserProfileResponse> CreateAsync(UserProfileRequest request)
        {
            var entity = _mapper.Map<UserProfile>(request);

            await _dbContext.UserProfiles.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.UserProfiles
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == entity.Id);

            return _mapper.Map<UserProfileResponse>(result);
        }

        // ✅ Get By Id
        public async Task<UserProfileResponse> GetByIdAsync(int id)
        {
            var entity = await _dbContext.UserProfiles
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return null;

            return _mapper.Map<UserProfileResponse>(entity);
        }

        // ✅ Get By UserId (1-to-1)
        public async Task<UserProfileResponse> GetByUserIdAsync(int userId)
        {
            var entity = await _dbContext.UserProfiles
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (entity == null)
                return null;

            return _mapper.Map<UserProfileResponse>(entity);
        }

        // ✅ Get All UserProfiles with Pagination + Search
        // This method returns paged results and supports searching across
        // UserProfile fields as well as related User fields (Name, Email).
        public async Task<PagedResults<UserProfileResponse>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? search = null)
        {
            // ⭐ Step 1: Build base query
            // Include(x => x.User) is required because:
            // - We need User.Name and User.Email for searching
            // - AutoMapper mapping also uses navigation property values
            // AsNoTracking() improves performance for read-only queries
            var query = _dbContext.UserProfiles
                .Include(x => x.User)
                .AsNoTracking()
                .AsQueryable();

            // ⭐ Step 2: Apply Search Filter (Optional)
            // This runs only when user provides a search string.
            // We convert search to lowercase for case-insensitive comparison.
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                // Search supports multiple fields:
                // - Address
                // - PhoneNumber
                // - Related User Name
                // - Related User Email
                query = query.Where(x =>
                    x.Address.ToLower().Contains(search) ||
                    x.PhoneNumber.ToLower().Contains(search) ||
                    x.User.Name.ToLower().Contains(search) ||
                    x.User.Email.ToLower().Contains(search));
            }

            // ⭐ Step 3: Get Total Count BEFORE Pagination
            // This is important for UI pagination:
            // Frontend uses TotalNumberOfRecords to calculate total pages.
            var totalCount = await query.CountAsync();

            // ⭐ Step 4: Apply Pagination
            // Skip: ignores records from previous pages
            // Take: limits number of records returned
            // Example:
            // pageNumber = 2, pageSize = 10 → Skip(10)
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)

                // ⭐ Step 5: Map Entity → Response DTO
                // Converts database model into API response model.
                .Select(x => _mapper.Map<UserProfileResponse>(x))
                .ToListAsync();

            // ⭐ Step 6: Return Paged Result Structure
            // This matches your PagedResults<T> format:
            // {
            //   pageNumber,
            //   pageSize,
            //   totalNumberOfRecords,
            //   results[]
            // }
            return new PagedResults<UserProfileResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalNumberOfRecords = totalCount,
                Results = data
            };
        }
        // ✅ Update
        public async Task<UserProfileResponse> UpdateAsync(int id, UserProfileRequest request)
        {
            var entity = await _dbContext.UserProfiles
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return null;

            _mapper.Map(request, entity);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserProfileResponse>(entity);
        }

        // ✅ Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.UserProfiles.FindAsync(id);
            if (entity == null)
                return false;

            _dbContext.UserProfiles.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}