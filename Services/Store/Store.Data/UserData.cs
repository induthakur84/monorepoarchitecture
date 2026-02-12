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
    public class UserData : IUserData
    {
        public readonly StoreDbContext _context;
        public readonly IMapper _mapper;
        public async Task<UserResponse> CreateAsync(UserRequest request)
        {
            var user = _mapper.Map<User>(request);
            _context.Users.Add(user);
            _context.SaveChanges();
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }


        /// <summary>
        /// AsNoTracking() is used to improve the performance of 
        /// read-only queries by disabling change tracking for the entities returned by the query.
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResults<UserResponse>> GetAllAsync()
        {
            var users = await _context.Users
                            .AsNoTracking()
                            .OrderByDescending(x => x.Id)
                            .ToListAsync();

            var totalCount = users.Count();
            var items = users
                .Select(x => _mapper.Map<UserResponse>(x))
                .ToList();
            return new PagedResults<UserResponse>
            {
                Results = items,
                TotalNumberOfRecords = totalCount,
            };
        }

        //"{}/ totalcount: 1000, results: []}"

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return null;
            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<UserResponse> UpdateAsync(int id, UserRequest request)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity == null)
                return null;
            _mapper.Map(request, entity);
            _context.Users.Update(entity); await _context.SaveChangesAsync();
            return _mapper.Map<UserResponse>(entity);
        }
    }
}
