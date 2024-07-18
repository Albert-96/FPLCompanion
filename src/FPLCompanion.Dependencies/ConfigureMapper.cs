using AutoMapper;
using FPLCompanion.Data.Entities;
using FPLCompanion.Data.ViewModels;
using FPLCompanion.Dto;
using FPLCompanion.Dto.ViewModels;

namespace FPLCompanion.Dependencies
{
    public class ConfigureMapper : Profile
    {
        public ConfigureMapper()
        {
            CreateMap<ElementDto, Element>().ReverseMap();
            CreateMap<Element, PlayerDetailViewDto>(MemberList.Source);
            CreateMap<PlayerDto, Element>().ReverseMap();
            CreateMap<ElementDto, ElementAggregate>().ReverseMap();
            CreateMap<TeamDto, Team>().ReverseMap();
            CreateMap<TeamViewModelDto, Team>().ReverseMap();
            CreateMap<ElementTypeDto, ElementType>().ReverseMap();
            CreateMap<FixtureDto, Fixture>().ReverseMap();
            CreateMap<FixtureViewModelDto, Fixture>().ReverseMap();
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
            CreateMap<ElementDetail, PlayerDetailViewDto>(MemberList.Source);
            CreateMap<HistoryDto, History>().ReverseMap();
            CreateMap<HistoryViewDto, History>().ReverseMap();
            CreateMap<HistoryPastDto, HistoryPast>().ReverseMap();
            CreateMap<HistoryPastViewDto, HistoryPast>().ReverseMap();
            CreateMap<DreamTeamDto, DreamTeam>().ReverseMap();
            CreateMap<TopPlayerDto, TopPlayer>()
                .ForMember(
                    dest => dest._id,
                    p => p.MapFrom(src => src.id))
                .ReverseMap();
            CreateMap<DreamElementDto, DreamElement>().ReverseMap();
            CreateMap<EventDetailDto, EventDetail>().ReverseMap();
            CreateMap<EventElementDto, EventElement>()
                .ForMember(
                    dest => dest.elementId,
                    p => p.MapFrom(src => src.id)
                ).ReverseMap();
            CreateMap<ExplainDto, Explain>().ReverseMap();
            CreateMap<ElementStatDto, ElementStat>().ReverseMap();
            CreateMap<ExplainStatDto, ExplainStat>().ReverseMap();
            CreateMap<ElementFixtureDto, ElementFixture>()
                .ForMember(
                    dest => dest._id,
                    p => p.MapFrom(src => src.id))
                .ReverseMap();
            CreateMap<ElementFixtureViewModelDto, ElementFixture>()
                .ForMember(
                    dest => dest._id,
                    p => p.MapFrom(src => src.id))
                .ReverseMap();
        }
    }
}
