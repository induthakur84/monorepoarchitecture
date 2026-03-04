using AutoMapper;
using Azure.Core;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
        
        //   Fact  attribute indicate like this method for testing purpose

        [Fact]
        public async  Task  CreateAsync_WithValidRequest_CratesUserSuccessfully()
        {
            //Arrange
            var request = new UserRequest
            {
                //Set the fields based on our UserRequest properties
                Name="Indu",
                Email="Indu@store.com"
            };
            //Act
            var result= await _sut.CreateAsync(request);
            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Indu");
            result.Email.Should().Be("Indu@store.com");
        }


        //Get by id

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsUser()
        {
            //Arrange
            var user = new User
            {
                //Set the fields based on our UserRequest properties
                Name = "test",
                Email = "Test@store.com"
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();


            ///Act
            ///
            var result= await _sut.GetByIdAsync(user.Id);




            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(user.Id);
        }
    }
}

//AAA

//A --Arrage --create (name=ram, email='ram@tmail.com'

//A--- Act  -- here we can hit the method(1, ram,ram@tmail.com)

//A-- Asset-----





