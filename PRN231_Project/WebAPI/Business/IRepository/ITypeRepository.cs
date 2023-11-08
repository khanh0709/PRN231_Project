using WebAPI.Business.DTO;

namespace WebAPI.Business.IRepository
{
    public interface ITypeRepository
    {
        public List<TypeDTO> GetTypes();
    }
}
