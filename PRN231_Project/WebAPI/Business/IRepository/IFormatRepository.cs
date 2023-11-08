using WebAPI.Business.DTO;

namespace WebAPI.Business.IRepository
{
    public interface IFormatRepository
    {
        public List<FormatDTO> GetFormats();
    }
}
