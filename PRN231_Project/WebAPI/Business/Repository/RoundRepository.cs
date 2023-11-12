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

        public void CreateRound(RoundDTO round)
        {
            RoundManager manager = new RoundManager(context);
            manager.CreateRound(mapper.Map<Round>(round));
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
