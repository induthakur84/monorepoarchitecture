using AutoMapper;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;
using Store.Domain.Entities;

namespace Store.Data.Automapper
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            // ✅ Request DTO → Entity
            CreateMap<UserProfileRequest, UserProfile>();

            // ✅ Entity → Response DTO
            CreateMap<UserProfile, UserProfileResponse>()
                // Map navigation property values from User table
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.User.Name))

                .ForMember(dest => dest.Email,
                           opt => opt.MapFrom(src => src.User.Email));
        }
    }
}