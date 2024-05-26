using AutoMapper;
using Secim2028.Dtos.AdayDto;
using Secim2028.Dtos.IttifakDto;
using Secim2028.Dtos.OyAdayDto;
using Secim2028.Dtos.OyPartiDto;
using Secim2028.Dtos.SandikDto;
using Secim2028.Dtos.SiyasiPartiDto;

namespace Secim2028.Profiles
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {

            CreateMap<AdayCumhurbaskani, AdayResponseDto>()
                .ForMember(dest => dest.AdayId, opt => opt.MapFrom(src => src.AdayId))
                .ForMember(dest => dest.AdayAdi, opt => opt.MapFrom(src => src.AdayAdi))
                .ForMember(dest => dest.SiyasiParti, opt => opt.MapFrom(src => src.SiyasiParti))
                .ReverseMap();

            CreateMap<AdayRequestDto, AdayCumhurbaskani>()
                .ForMember(dest => dest.AdayAdi, opt => opt.MapFrom(src => src.AdayAdi))
                .ReverseMap();


            CreateMap<SiyasiParti, SiyasiPartiResponseDto>()
                .ForMember(dest => dest.Ittifak, opt => opt.MapFrom(src => src.Ittifak))
                .ReverseMap();

            CreateMap<SiyasiPartiRequestDto, SiyasiParti>();

            CreateMap<SiyasiParti, SiyasiPartiResponseOfIttifakDto>()
                .ForMember(dest => dest.SiyasiPartiId, opt => opt.MapFrom(src => src.SiyasiPartiId))
                .ForMember(dest => dest.SiyasiPartiAdi, opt => opt.MapFrom(src => src.SiyasiPartiAdi))
                .ForMember(dest => dest.SiyasiPartiKisaltma, opt => opt.MapFrom(src => src.SiyasiPartiKisaltma))
                .ReverseMap();

            CreateMap<Sandik, SandikResponseDto>()
                .ForMember(dest => dest.SandikId, opt => opt.MapFrom(src => src.SandikId))
                .ForMember(dest => dest.SandikNo, opt => opt.MapFrom(src => src.SandikNo))
                .ReverseMap();


            CreateMap<Ittifak, IttifakResponseDto>()
                .ForMember(dest => dest.IttifakId, opt => opt.MapFrom(src => src.IttifakId))
                .ForMember(dest => dest.SiyasiPartis, opt => opt.MapFrom(src => src.SiyasiPartiler))
                .ReverseMap();

            CreateMap<IttifakRequestDto, Ittifak>()
                .ForMember(dest => dest.IttifakAdi, opt => opt.MapFrom(src => src.IttifakAdi))
                .ReverseMap();

            CreateMap<Ittifak, IttifakResponseOfSiyasiPartiDto>()
                .ForMember(dest => dest.IttifakId, opt => opt.MapFrom(src => src.IttifakId))
                .ForMember(dest => dest.IttifakAdi, opt => opt.MapFrom(src => src.IttifakAdi))
                .ReverseMap();

            CreateMap<Ittifak, IttifakResponseOfOyOranDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IttifakId))
                .ForMember(dest => dest.IttifakName, opt => opt.MapFrom(src => src.IttifakAdi))
                .ReverseMap();



            CreateMap<OyAday,OyAdayResponseDto>()
                .ForMember(dest => dest.Aday, opt => opt.MapFrom(src => src.CumhurbaskaniAdayi))
                .ForMember(dest => dest.OySayisi, opt => opt.MapFrom(src => src.OySayisi))
                .ReverseMap();

            CreateMap<AdayCumhurbaskani, AdayResponseForOyAdayDto>()
                .ForMember(dest => dest.AdayId, opt => opt.MapFrom(src => src.AdayId))
                .ForMember(dest => dest.AdayAdi, opt => opt.MapFrom(src => src.AdayAdi))
                .ReverseMap();

            CreateMap<OyParti, OyPartiResponseDto>()
                .ForMember(dest => dest.SiyasiParti, opt => opt.MapFrom(src => src.SiyasiParti))
                .ForMember(dest => dest.OySayisi, opt => opt.MapFrom(src => src.OySayisi))
                .ReverseMap();

            CreateMap<SiyasiParti, SiyasiPartiForOyPartiDto>()
                .ForMember(dest => dest.siyasiPartiId, opt => opt.MapFrom(src => src.SiyasiPartiId))
                .ForMember(dest => dest.siyasiPartiName, opt => opt.MapFrom(src => src.SiyasiPartiAdi))
                .ForMember(dest => dest.siyasiPartiKisaltma, opt => opt.MapFrom(src => src.SiyasiPartiKisaltma))
                .ReverseMap();


            CreateMap<Ilce, ilceResponseDto>();
            CreateMap<Il, IlDto>();
        }
    }
}
