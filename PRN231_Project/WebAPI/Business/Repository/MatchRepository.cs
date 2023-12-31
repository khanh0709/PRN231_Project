﻿using AutoMapper;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebAPI.Business.Mapping;
using WebAPI.DataAccess.Manager;
using WebAPI.DataAccess.Models;

namespace WebAPI.Business.Repository
{
    public class MatchRepository : IMatchRepository
    {
        CoFABContext context;
        Mapper mapper;
        public MatchRepository(CoFABContext context)
        {
            this.context = context;
            mapper = MapperConfig.InitializeAutomapper();
        }
        public List<MatchDTO> GetMatchesByRoundId(int roundId)
        {
            MatchManager manger = new MatchManager(context);
            List<Match> list = manger.GetMatchesByRoundId(roundId);
            return mapper.Map<List<MatchDTO>>(list);  
        }

        public void SaveMatches(int roundId, List<int?> player1Id, List<int?> player2Id, List<int?> winerId)
        {
            MatchManager manger = new MatchManager(context);
            manger.SaveMatches(roundId, player1Id, player2Id, winerId);
        }
    }
}
