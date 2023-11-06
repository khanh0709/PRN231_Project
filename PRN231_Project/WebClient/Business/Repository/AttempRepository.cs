using AutoMapper;
using CoFAB.Business.DTO;
using CoFAB.Business.IRepository;
using CoFAB.Business.Mapping;
using CoFAB.DataAccess.Manager;
using CoFAB.DataAccess.Models;

namespace CoFAB.Business.Repository
{
    public class AttempRepository : IAttempRepository
    {
        CoFABContext context;
        Mapper mapper;
        public AttempRepository(CoFABContext context)
        {
            this.context = context;
            mapper = MapperConfig.InitializeAutomapper();
        }
        public List<AttempDTO> GetPlayers(int tournamentId, bool accepted)
        {
            AttempManager manager = new AttempManager(context);
            List<Attemp> list = manager.GetPlayers(tournamentId, accepted);
            return mapper.Map<List<AttempDTO>>(list);   
        }

        public void UpdateAcceptAttemp(int attempId, bool accepted)
        {
            AttempManager manager = new AttempManager(context);
            manager.UpdateAcceptAttemp(attempId, accepted);
        }

        public AttempDTO GetAttempById(int attempId)
        {
            AttempManager manager = new AttempManager(context);
            Attemp a = manager.GetAttempById(attempId);
            return mapper.Map<AttempDTO>(a);
        }

        public void AddPlayers(int tournamentId, List<int> playerId)
        {
            AttempManager manager = new AttempManager(context);
            manager.AddPlayers(tournamentId, playerId);
        }

        public bool IsValidAcceptAndRemoveAttemp(int attempId)
        {
            AttempManager manager = new AttempManager(context);
            return manager.IsValidAcceptAndRemoveAttemp(attempId);
        }

        public void CalXp(int tourId)
        {
            AttempManager manager = new AttempManager(context);
            manager.CalXp(tourId);
        }

        public bool ValidCalXp(int tourId)
        {
            AttempManager manager = new AttempManager(context);
            return manager.ValidCalXp(tourId);
        }

        public void CreateRequest(int tourId, int userId)
        {
            AttempManager manager = new AttempManager(context);
            manager.CreateRequest(tourId, userId);
        }

        public bool ValidRequest(int tourId, int userId)
        {
            AttempManager manager = new AttempManager(context);
            return manager.ValidRequest(tourId, userId);
        }
    }
}
