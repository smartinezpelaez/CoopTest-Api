using AutoMapper;
using CoopTest.DAL.Models;

namespace CoopTest.BLL.DTOs
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Fondo, FondoDTO>().ReverseMap();
            CreateMap<Transaccion, TransaccionDTO>().ReverseMap();
            CreateMap<FondoVinculado, FondoVinculadoDTO>().ReverseMap();            

            CreateMap<Fondo, FondoVinculado>()
                .ForMember(dest => dest.FechaVinculacion, opt => opt.Ignore());

            CreateMap<SuscripcionFondoDTO, Transaccion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TipoTransaccion, opt => opt.Ignore())
                .ForMember(dest => dest.FechaTransaccion, opt => opt.Ignore())
                .ForMember(dest => dest.Monto, opt => opt.Ignore());

            //CreateMap<SuscripcionFondoDTO, Transaccion>()
            //.ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto));
            //CreateMap<Fondo, FondoVinculado>()
            //    .ForMember(dest => dest.IdFondo, opt => opt.MapFrom(src => src.Id));

            CreateMap<FondoVinculado, Transaccion>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.TipoTransaccion, opt => opt.MapFrom(src => "Cancelación"))
            .ForMember(dest => dest.FechaTransaccion, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.MontoMinimo));
        }
    }
}
