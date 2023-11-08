using AutoMapper;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebAPI.Business.Mapping;
using WebAPI.DataAccess.Manager;
using WebAPI.DataAccess.Models;

namespace WebAPI.Business.Repository
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
