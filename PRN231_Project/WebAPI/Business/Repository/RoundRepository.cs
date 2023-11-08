using AutoMapper;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebAPI.Business.Mapping;
using WebAPI.DataAccess.Manager;
using WebAPI.DataAccess.Models;

namespace WebAPI.Business.Repository
{
    public class RoundRepository : IRoundRepository
    {
        CoFABContext context;
        Mapper mapper;
        public RoundRepository(CoFABContext context)
        {
            this.context = context;
            mapper = MapperConfig.InitializeAutomapper();
        }

        public void CreateRound(int tournamentId, string roundName, int matchNumber)
        {
            RoundManager manager = new RoundManager(context);
            manager.CreateRound(tournamentId, roundName, matchNumber);
        }

        public List<RoundDTO> GetRoundByTournamentId(int tourId)
        {
            RoundManager manager = new RoundManager(context);
            List<Round> rounds = manager.GetRoundByTournamentId(tourId);
            return mapper.Map<List<RoundDTO>>(rounds);    
        }

        public void DeleteRound(int roundId)
        {
            RoundManager manager = new RoundManager(context);   
            manager.DeleteRound(roundId);   
        }
    }
}
