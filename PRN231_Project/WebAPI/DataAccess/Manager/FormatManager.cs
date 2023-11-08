using WebAPI.DataAccess.Models;

namespace WebAPI.DataAccess.Manager
{
    public class FormatManager
    {
        CoFABContext context;
        public FormatManager(CoFABContext context)
        {
            this.context = context;
        }
        public List<Format> GetFormats()
        {
            return context.Formats.ToList();
        }
    }
}
