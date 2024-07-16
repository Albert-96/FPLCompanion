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
            CreateMap<FixtureDto, Fixture>().ReverseMap();
            CreateMap<FixtureStatDto, FixtureStat>().ReverseMap();
            CreateMap<FixturePlayerDto, FixturePlayer>().ReverseMap();
            CreateMap<EventDto, Event>().ReverseMap();
            CreateMap<ChipPlayDto, ChipPlay>().ReverseMap();
            CreateMap<TopElementInfoDto, TopElementInfo>()
                .ForMember(
                    dest => dest._id,
                    p => p.MapFrom(src => src.id))
                .ReverseMap();
            CreateMap<ElementDetailDto, ElementDetail>().ReverseMap();
            CreateMap<HistoryDto, History>().ReverseMap();
            CreateMap<HistoryPastDto, HistoryPast>().ReverseMap();
            CreateMap<DreamTeamDto, DreamTeam>().ReverseMap();
            CreateMap<TopPlayerDto, TopPlayer>()
                .ForMember(
                    dest => dest._id,
                    p => p.MapFrom(src => src.id))
                .ReverseMap();
            CreateMap<DreamElementDto, DreamElement>().ReverseMap();
        }
    }
}
