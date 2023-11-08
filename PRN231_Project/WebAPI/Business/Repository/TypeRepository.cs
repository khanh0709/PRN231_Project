using AutoMapper;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebAPI.Business.Mapping;
using WebAPI.DataAccess.Manager;
using WebAPI.DataAccess.Models;

namespace WebAPI.Business.Repository
{
    public class TypeRepository : ITypeRepository
    {
        CoFABContext context;
        Mapper mapper;
        public TypeRepository(CoFABContext context)
        {
            this.context = context;
            mapper = MapperConfig.InitializeAutomapper();
        }

        public List<TypeDTO> GetTypes()
        {
            TypeManager manager = new TypeManager(context);
            List<DataAccess.Models.Type> types = manager.GetTypes();
            return mapper.Map<List<TypeDTO>>(types);
        }
    }
}
