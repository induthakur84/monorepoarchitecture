using AutoMapper;
using Store.Domain.DTO.Request;
using Store.Domain.DTO.Response;
using Store.Domain.Entities;

namespace Store.Data.Automapper
{
    public class OrderMapping : Profile
    {

        public OrderMapping()
        {
            // ✅ Request DTO -> Entity
            CreateMap<OrderRequest, Order>();

            // ✅ Entity -> Response DTO
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UserEmail,
                    opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
