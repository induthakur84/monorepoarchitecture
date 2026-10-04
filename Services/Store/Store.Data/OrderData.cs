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

    //encapsulation 
    public class OrderData : IOrderData
    {
        private readonly StoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderData(StoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // ✅ Create
        public async Task<OrderResponse> CreateAsync(OrderRequest request)
        {
            // Optional: validate User exists
            var userExists = await _dbContext.Users.AnyAsync(x => x.Id == request.UserId);
            if (!userExists)
                return null;

            var entity = _mapper.Map<Order>(request);

            await _dbContext.Orders.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Orders
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == entity.Id);
          
            var result= await _context.Orders
                .Include(x=>x.User)
                .FirstOrDefaultAsync(x => x.Id == order.Id);
            return _mapper.Map<OrderResponse>(result);
        }

        // ✅ Get By Id
        public async Task<OrderResponse> GetByIdAsync(int id)
            {
            var entity = await _dbContext.Orders
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return null;

            return _mapper.Map<OrderResponse>(entity);
        }

        // ✅ Get All Orders (Pagination + Search)
        public async Task<PagedResults<OrderResponse>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10, 
     string? search = null,
     string? sortBy = null,
     string? sortOrder = "desc")
        {
            var query = _dbContext.Orders
                .Include(x => x.User)
                .AsNoTracking()
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                query = query.Where(x => 
                    x.User.Name.ToLower().Contains(search) ||
                    x.User.Email.ToLower().Contains(search));
            }

            // Sorting
            query = (sortBy?.ToLower(), sortOrder?.ToLower()) switch
            {
                ("id", "asc") => query.OrderBy(x => x.Id),
                ("id", "desc") => query.OrderByDescending(x => x.Id),
                _ => query.OrderByDescending(x => x.Id)
            };

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => _mapper.Map<OrderResponse>(x))
                .ToListAsync();

            return new PagedResults<OrderResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalNumberOfRecords = totalCount,
                Results = data
            };
        }
        // ✅ Get Orders By UserId (Pagination + Search)
        public async Task<PagedResults<OrderResponse>> GetByUserIdAsync(
            int userId, 
            int pageNumber = 1,
            int pageSize = 10,
            string? search = null)
        {
            var query = _dbContext.Orders
                .Include(x => x.User)

                // this is to improve the performance by not tracking the changes in the entities
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(x =>
                    x.User.Name.ToLower().Contains(search) ||
                    x.User.Email.ToLower().Contains(search));
            }

            var totalCount = await query.CountAsync();


            var data = await query
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => _mapper.Map<OrderResponse>(x))
                .ToListAsync();

            return new PagedResults<OrderResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalNumberOfRecords = totalCount,
                Results = data
            };
        }

        // ✅ Update
        public async Task<OrderResponse> UpdateAsync(int id, OrderRequest request)
        {
            var entity = await _dbContext.Orders
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return null;

            // Optional: validate new user
            var userExists = await _dbContext.Users.AnyAsync(x => x.Id == request.UserId);
            if (!userExists)
                return null;

            _mapper.Map(request, entity);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OrderResponse>(entity);
        }

        // ✅ Delete
        public async Task<bool> DeleteAsync(int id)
            {
            var entity = await _dbContext.Orders.FindAsync(id);
            if (entity == null)
                return false;

            _dbContext.Orders.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
