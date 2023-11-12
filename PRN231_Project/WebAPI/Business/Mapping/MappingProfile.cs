using WebAPI.Business.DTO;
using WebAPI.DataAccess.Models;
using System.Security.Claims;

namespace WebAPI.Business.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Attemp, AttempDTO>()
                .ForMember(des => des.Tournament,
                act => act.MapFrom(src => src.Tournament))
                .ForMember(des => des.User,
                act => act.MapFrom(src => src.User));

            CreateMap<Match, MatchDTO>()
                .ForMember(des => des.Round,
                act => act.MapFrom(src => src.Round))
                .ForMember(des => des.Player1Navigation,
                act => act.MapFrom(src => src.Player1Navigation))
                .ForMember(des => des.Player2Navigation,
                act => act.MapFrom(src => src.Player2Navigation))
                .ForMember(des => des.WinerNavigation,
                act => act.MapFrom(src => src.WinerNavigation));

            CreateMap<Round, RoundDTO>()
                .ForMember(des => des.Tournament,
                act => act.MapFrom(src => src.Tournament))
                .ForMember(des => des.Matches,
                act => act.MapFrom(src => src.Matches))
                .ReverseMap();

            CreateMap<Tournament, TournamentDTO>()
                .ForMember(des => des.User,
                act => act.MapFrom(src => src.User))
                .ForMember(des => des.Attemps,
                act => act.MapFrom(src => src.Attemps))
                .ForMember(des => des.Rounds,
                act => act.MapFrom(src => src.Rounds))
                .ForMember(des => des.Type,
                act => act.MapFrom(src => src.Type))
                .ForMember(des => des.Format,
                act => act.MapFrom(src => src.Format));

            CreateMap<User, UserDTO>()
                .ForMember(des => des.Attemps,
                act => act.MapFrom(src => src.Attemps))
                .ForMember(des => des.MatchPlayer1Navigations,
                act => act.MapFrom(src => src.MatchPlayer1Navigations))
                .ForMember(des => des.MatchPlayer2Navigations,
                act => act.MapFrom(src => src.MatchPlayer2Navigations))
                .ForMember(des => des.MatchWinerNavigations,
                act => act.MapFrom(src => src.MatchWinerNavigations))
                .ForMember(des => des.Tournaments,
                act => act.MapFrom(src => src.Tournaments));

            CreateMap<DataAccess.Models.Type, TypeDTO>();
            CreateMap<Format, FormatDTO>();
            //
            CreateMap<TournamentDTO, Tournament>()
                .ForMember(des => des.User,
                act => act.MapFrom(src => src.User))
                .ForMember(des => des.Attemps,
                act => act.MapFrom(src => src.Attemps))
                .ForMember(des => des.Rounds,
                act => act.MapFrom(src => src.Rounds))
                .ForMember(des => des.Type,
                act => act.MapFrom(src => src.Type))
                .ForMember(des => des.Format,
                act => act.MapFrom(src => src.Format));

            CreateMap<UserDTO, User>()
                .ForMember(des => des.Attemps,
                act => act.MapFrom(src => src.Attemps))
                .ForMember(des => des.MatchPlayer1Navigations,
                act => act.MapFrom(src => src.MatchPlayer1Navigations))
                .ForMember(des => des.MatchPlayer2Navigations,
                act => act.MapFrom(src => src.MatchPlayer2Navigations))
                .ForMember(des => des.MatchWinerNavigations,
                act => act.MapFrom(src => src.MatchWinerNavigations))
                .ForMember(des => des.Tournaments,
                act => act.MapFrom(src => src.Tournaments));
        }
    }
}
