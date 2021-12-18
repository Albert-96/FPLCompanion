using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.Data.ViewModels;

namespace FPLCompanion.Dependencies
{
    public class ConfigureMapper : Profile
    {
        public ConfigureMapper()
        {
            CreateMap<ElementDto, Element>().ReverseMap();
        }
    }
}
