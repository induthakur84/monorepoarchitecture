using AutoMapper;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;
using Store.Domain.Entities;

namespace Store.Data.Automapper
{
    public class UserMapping: Profile
    {
        public UserMapping()
        {
            // Request DTO → Entity
            CreateMap<UserRequest, User>();

            // Entity → Response DTO
            CreateMap<User, UserResponse>();

         
        }
    }
}
