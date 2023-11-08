using AutoMapper;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebAPI.Business.Mapping;
using WebAPI.DataAccess.Manager;
using WebAPI.DataAccess.Models;

namespace WebAPI.Business.Repository;
public class TournamentRepository : ITournamentRepository
{
    private CoFABContext context;
    private Mapper       mapper;

    public TournamentRepository(CoFABContext context)
    {
        this.context = context;
        this.mapper  = MapperConfig.InitializeAutomapper();
    }

    public List<TournamentDTO> GetTournamentsByUser(int userId)
    {
        var manager     = new TournamentManager(this.context);
        var tournaments = manager.GetTournamentsByUser(userId);
        return this.mapper.Map<List<TournamentDTO>>(tournaments);
    }


    public void CreateTournament(TournamentDTO tour)
    {
        var manager = new TournamentManager(this.context);
        manager.CreateTournament(this.mapper.Map<Tournament>(tour));
    }

    public TournamentDTO GetTournamentsByIdAndUser(int id, int userId)
    {
        var manager    = new TournamentManager(this.context);
        var tournament = manager.GetTournamentsByIdAndUser(id, userId);
        return this.mapper.Map<TournamentDTO>(tournament);
    }

    public void UpdateInfoTournament(int id, string name, int typeId, int formatId, DateTime startTime, string? description, string address, double xpmodifier)
    {
        var manager = new TournamentManager(this.context);
        manager.UpdateInfoTournament(id, name, typeId, formatId, startTime, description, address, xpmodifier);
    }

    public void DeleteTournament(int id)
    {
        var manager = new TournamentManager(this.context);
        manager.DeleteTournament(id);
    }

    public bool ValidAcceptAndRemoveTournament(int tourId)
    {
        var manager = new TournamentManager(this.context);
        return manager.ValidAcceptAndRemoveTournament(tourId);
    }

    public List<TournamentDTO> GetUpComingTournament(int userId)
    {
        var manager = new TournamentManager(this.context);
        var list    = this.mapper.Map<List<TournamentDTO>>(manager.GetUpComingTournament(userId));
        var newList = new List<TournamentDTO>();
        foreach (var tour in list)
        {
            var isRegistered = false;
            foreach (var attemp in tour.Attemps)
            {
                //neu co attemp == userid && accepted = false
                if (attemp.UserId == userId && attemp.Accepted == true)
                {
                    isRegistered = true;
                }
            }

            if (!isRegistered)
            {
                newList.Add(tour);
            }
        }

        return newList;
    }

    public List<TournamentDTO> GetRegisteredTournament(int userId)
    {
        var manager = new TournamentManager(this.context);
        var list    = this.mapper.Map<List<TournamentDTO>>(manager.GetRegisteredTournament(userId));
        foreach (var tour in list)
        {
            tour.TotalWins = tour.Attemps.Where(a => a.UserId == userId && a.Accepted == true).Sum(x => x.TotalWins ?? 0);
            tour.Xpgained  = tour.Attemps.Where(a => a.UserId == userId && a.Accepted == true).Sum(x => x.Xpgained ?? 0);
        }

        return list;
    }

    public List<TournamentDTO> GetEndTournament(int userId)
    {
        var manager = new TournamentManager(this.context);
        var list    = this.mapper.Map<List<TournamentDTO>>(manager.GetEndTournament(userId));
        foreach (var tour in list)
        {
            tour.TotalWins = tour.Attemps.Where(a => a.UserId == userId && a.Accepted == true).Sum(x => x.TotalWins ?? 0);
            tour.Xpgained  = tour.Attemps.Where(a => a.UserId == userId && a.Accepted == true).Sum(x => x.Xpgained ?? 0);
        }

        return list;
    }

    public void UpdateStatus(int tourId, int status)
    {
        var manager = new TournamentManager(this.context);
        manager.UpdateStatus(tourId, status);
    }
}