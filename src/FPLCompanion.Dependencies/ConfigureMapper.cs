using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.Data.ViewModels;
using FPLCompanion.Dto;

namespace FPLCompanion.Dependencies
{
    public class ConfigureMapper : Profile
    {
        public ConfigureMapper()
        {
            CreateMap<ElementDto, Element>().ReverseMap();
            CreateMap<ElementDto, ElementAggregate>().ReverseMap();
            CreateMap<TeamDto, Team>().ReverseMap();
            CreateMap<ElementTypeDto, ElementType>().ReverseMap();
        }
    }
}
