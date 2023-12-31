﻿namespace WebAPI.DataAccess.Manager;

using WebAPI.Business.Enums;
using WebAPI.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

public class TournamentManager
{
    private CoFABContext context;

    public TournamentManager(CoFABContext context)
    {
        this.context = context;
    }

    public Tournament GetTournamentById(int tourId)
    {
        return this.context.Tournaments
            .Include(t => t.Type).Include(t => t.Format)
            .FirstOrDefault(t => t.TournamentId == tourId && t.Deleted == false);
    }

    public List<Tournament> GetTournamentsByUser(int userId)
    {
        return this.context.Tournaments.Where(t => t.UserId == userId && t.Deleted == false).Include(t => t.Type).Include(t => t.Format).ToList();
    }

    public void CreateTournament(Tournament tour)
    {
        this.context.Tournaments.Add(tour);
        this.context.SaveChanges();
    }

    public Tournament GetTournamentsByIdAndUser(int id, int userId)
    {
        return this.context.Tournaments.Include(t => t.Type).Include(t => t.Format).FirstOrDefault(t => t.UserId == userId && t.TournamentId == id && t.Deleted == false);
    }

    public void UpdateInfoTournament(Tournament tournament)
    {
        var tour = this.context.Tournaments.FirstOrDefault(t => t.TournamentId == tournament.TournamentId);
        if (tour != null)
        {
            tour.Name        = tournament.Name;
            tour.TypeId      = tournament.TypeId;
            tour.FormatId    = tournament.FormatId;
            tour.StartTime   = tournament.StartTime;
            tour.Description = tournament.Description;
            tour.Address     = tournament.Address;
            tour.Xpmodifier  = tournament.Xpmodifier;

            this.context.Tournaments.Update(tour);
            this.context.SaveChanges();
        }
    }

    public void DeleteTournament(int id)
    {
        var tour = this.context.Tournaments.FirstOrDefault(t => t.TournamentId == id);
        if (tour != null)
        {
            tour.Deleted = true;

            this.context.Tournaments.Update(tour);
            this.context.SaveChanges();
        }
    }

    public bool ValidAcceptAndRemoveTournament(int tourId)
    {
        var rounds = this.context.Rounds.Where(r => r.TournamentId == tourId);
        if (rounds != null && rounds.Count() > 0)
        {
            return false;
        }

        return true;
    }

    public List<Tournament> GetUpComingTournament(int userId)
    {
        return this.context.Tournaments
                   .Where(t => t.Status == (int)TournamentStatus.UpComing && t.Deleted == false)
                   .Include(t => t.Attemps)
                   .ToList();
    }

    public List<Tournament> GetRegisteredTournament(int userId)
    {
        return this.context.Attemps
                   .Include(a => a.Tournament.Type)
                   .Include(a => a.Tournament.User)
                   .Include(a => a.Tournament.Rounds)
                   .ThenInclude(r => r.Matches)
                   .Where(a => a.UserId == userId && a.Accepted == true)
                   .Select(a => a.Tournament)
                   .Where(t => t.Status != (int)TournamentStatus.End && t.Deleted == false)
                   .ToList();
    }

    public List<Tournament> GetEndTournament(int userId)
    {
        return this.context.Attemps
                   .Where(a => a.UserId == userId && a.Accepted == true)
                   .Include(a => a.Tournament.Type)
                   .Include(a => a.Tournament.User)
                   .Include(a => a.Tournament.Rounds)
                   .ThenInclude(r => r.Matches)
                   .Select(a => a.Tournament)
                   .Where(t => t.Status == (int)TournamentStatus.End && t.Deleted == false)
                   .ToList();
    }

    public void UpdateStatus(int tourId, int status)
    {
        var tour = this.context.Tournaments.FirstOrDefault(t => t.TournamentId == tourId);
        if (tour != null)
        {
            tour.Status = status;
            this.context.Tournaments.Update(tour);
            this.context.SaveChanges();
        }
    }
}