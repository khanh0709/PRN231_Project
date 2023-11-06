using CoFAB.Business.DTO;

namespace CoFAB.Business.IRepository
{
    public interface ITypeRepository
    {
        public List<TypeDTO> GetTypes();
    }
}
