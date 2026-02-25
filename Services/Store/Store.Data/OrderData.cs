using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedModel;
using Store.Data.Context;
using Store.Data.Interfaces;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;

namespace Store.Data
{

    //Which oops concept is used in this class?
    //Inheritance is used in this class
    //because it implements the IOrderData interface,
    //which defines the contract for the operations
    //that can be performed on orders.
    //By implementing this interface,
    //the OrderData class provides concrete implementations for the methods defined in IOrderData,
    //allowing it to be used polymorphically wherever an IOrderData type is expected. This promotes code reusability and separation of concerns, as the OrderData class can focus on the specific logic for handling orders while adhering to a common interface.

    //2 oops concepts are used in this class:

    //Encapsulation is used in this class because the internal workings of the OrderData
    //class are hidden from the outside world. The class provides public methods for creating,
    //retrieving, updating, and deleting orders, but the actual implementation details are
    //encapsulated within the class. This allows for better modularity and maintainability
    //, as changes to the internal logic of the OrderData class
    //do not affect other parts of the application that rely on its public interface.



    //Encapsulation means wrapping the data and method together as a single unit.
    public class OrderData : IOrderData
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;
        public OrderData(StoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Create a new order
        public  async Task<OrderResponse> Create(OrderRequest request)
        {
           var order = _mapper.Map<Domain.Entities.Order>(request);
           await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
          
            var result= await _context.Orders
                .Include(x=>x.User)
                .FirstOrDefaultAsync(x => x.Id == order.Id);
            return _mapper.Map<OrderResponse>(result);
        }

        public Task<bool> Delete(int id)
        {
           var order= _context.Orders.Find(id);
            if(order == null)
            {
                return Task.FromResult(false);
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Task.FromResult(true);
        }


        //Get All Orders for all users(With Pagination and Searching)
        public async Task<PagedResults<OrderResponse>> GetAll(
            int pageNumber = 1,
            int pageSize = 10, 
            string? searchTerm = null)
        {
            var query= _context.Orders
                .Include(x => x.User)
                .AsNoTracking()
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x => 
                x.User.Name.Contains(searchTerm) ||
                x.User.Email.Contains(searchTerm));
            }

            var totalRecords = await query.CountAsync();
            var data= await query
                .OrderByDescending(x=>x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => _mapper.Map<OrderResponse>(x))
                .ToListAsync();

            return new PagedResults<OrderResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalNumberOfRecords = totalRecords,
                Results = data
            };
        }

        public async Task<PagedResults<OrderResponse>> GetAllByUserId(
            int userId, 
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _context.Orders

                // this is the filter to get orders for a specific user
                .Where(x => x.UserId == userId)

                // this is the filter to search orders by user name or email
                .Include(x => x.User)

                // this is to improve the performance by not tracking the changes in the entities
                .AsNoTracking()

                // this is to make the queryable so that we can apply pagination and searching on it
                .AsQueryable();
            if(!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x =>
                x.User.Name.Contains(searchTerm) ||
                x.User.Email.Contains(searchTerm));
            }

            var totalRecords = await query.CountAsync();


            var data = await query
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => _mapper.Map<OrderResponse>(x))
                .ToListAsync();

            return new PagedResults<OrderResponse>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalNumberOfRecords = totalRecords,
                Results = data
            };
        }


        //Get by Id means get order by id
        public async Task<OrderResponse> GetById(int id)
        {
            var order =await _context.Orders
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
            {
                return null;
            }
            return _mapper.Map<OrderResponse>(order);

        }

        public  async Task<OrderResponse> Update(int id, OrderRequest request)
        {
           var order =  await _context.Orders
                .Include(x=>x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            if(order == null)
            {
                return null;
            }

            _mapper.Map(request, order);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderResponse>(order);

        }
    }
}
