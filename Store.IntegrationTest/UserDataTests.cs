using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Data.Context;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;
using Store.Domain.Entities;

namespace Store.IntegrationTest
{




    //IDisposable is user automatically test case
    public class UserDataTests : IDisposable
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserData _sut;

        public UserDataTests()
        {
            // here we  make fake database from testing purpose
            var options = new DbContextOptionsBuilder<StoreDbContext>()

                // here it used to make inmemory database(new db per test)
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new StoreDbContext(options);
            //Automapper Configuration
            //replace userautomapperprofile with this profile

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRequest, User>();
                cfg.CreateMap<User, UserResponse>();
            });
            _mapper= mapperConfig.CreateMapper();
            _sut = new UserData(_context, _mapper);
        }

        public void Dispose()
        {
           _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
//AAA

//A --Arrage --create (name=ram, email='ram@tmail.com'

//A--- Act  -- here we can hit the method(1, ram,ram@tmail.com)

//A-- Asset-----
