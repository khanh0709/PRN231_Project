using AutoMapper;
using CoFAB.Business.DTO;
using CoFAB.Business.IRepository;
using CoFAB.Business.Mapping;
using CoFAB.DataAccess.Manager;
using CoFAB.DataAccess.Models;

namespace CoFAB.Business.Repository
{
    public class FormatRepository : IFormatRepository
    {
        CoFABContext context;
        Mapper mapper;
        public FormatRepository(CoFABContext context)
        {
            this.context = context;
            mapper = MapperConfig.InitializeAutomapper();
        }

        public List<FormatDTO> GetFormats()
        {
            FormatManager manager = new FormatManager(context);
            List<Format> formats = manager.GetFormats();
            return mapper.Map<List<FormatDTO>>(formats);  
        }
    }
}
