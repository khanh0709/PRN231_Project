using AutoMapper;
using CoFAB.Business.DTO;
using CoFAB.Business.IRepository;
using CoFAB.Business.Mapping;
using CoFAB.DataAccess.Manager;
using CoFAB.DataAccess.Models;

namespace CoFAB.Business.Repository
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
