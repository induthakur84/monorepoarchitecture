using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedModel;
using Store.Data.Context;
using Store.Data.Interfaces;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;
using Store.Domain.Entities;

namespace Store.Data.Services
{

    //Integration testing
    public class UserData : IUserData
    {

        // business logic here

        // Encapsulation
        private readonly StoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserData(StoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserResponse> CreateAsync(UserRequest request)
        {
            // Optional simple validation
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required");

            // Optional: if Name is unique in DB, prevent duplicate early
            var isNameExists = await _dbContext.Users
                .AnyAsync(x => x.Name.ToLower() == request.Name.ToLower());

            if (isNameExists)
                throw new InvalidOperationException("User name already exists");

            var entity = _mapper.Map<User>(request);

            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new ApplicationException($"User not found with id: {id}");

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<PagedResults<UserResponse>> GetAllAsync()
        {
            var users = await _dbContext.Users
                            .AsNoTracking()
                            .OrderByDescending(x => x.Id)
                            .ToListAsync();

            var totalCount = users.Count();

            var items = users
                .Select(u => _mapper.Map<UserResponse>(u))
                .ToList();

            return new PagedResults<UserResponse>
            {
                Results = items,
                TotalNumberOfRecords = totalCount
            };
        }
        public async Task<UserResponse> UpdateAsync(int id, UserRequest request)
        {
            var entity = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new ApplicationException($"User not found with id: {id}");

            // If Name is unique, avoid duplicate name for other users
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var duplicate = await _dbContext.Users.AnyAsync(x =>
                    x.Id != id &&
                    x.Name.ToLower() == request.Name.ToLower());

                if (duplicate)
                    throw new InvalidOperationException("User name already exists");
            }

            // Map request → existing entity (Update)
            _mapper.Map(request, entity);

            await _dbContext.SaveChangesAsync();

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return null;
            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return false;

            _dbContext.Users.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
