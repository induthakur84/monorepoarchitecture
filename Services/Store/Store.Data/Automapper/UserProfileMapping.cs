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
            // Request dto to convert to entity

            CreateMap<UserProfileRequest, UserProfile>();

            // Covert entity to userprofileresponse
            CreateMap<UserProfile, UserProfileResponse>()
                .ForMember(dest => dest.UserName,
                                  opt => opt.MapFrom(src => src.User.Name))
                 .ForMember(dest => dest.UserEmail,
                                  opt => opt.MapFrom(src => src.User.Email))
                  .ForMember(dest => dest.UserAddress,
                                  opt => opt.MapFrom(src => src.Address))
                  .ForMember(dest => dest.UserPhoneNumber,
                                  opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
//paid//manual mapping