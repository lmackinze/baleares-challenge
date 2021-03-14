using AutoMapper;
using BalearesChallengeApp.Models.DTOs;
using BalearesChallengeApp.Models.Entities;

namespace BalearesChallengeApp.Models.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Contact, ContactDto>()
                .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.City.Province.Name));

            CreateMap<ContactDto, Contact>();

            CreateMap<Contact, Contact>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOnUtc, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, opt => opt.Ignore())
                ;
        }
    }
}
