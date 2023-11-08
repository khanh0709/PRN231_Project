using WebAPI.DataAccess.Models;

namespace WebAPI.DataAccess.Manager
{
    public class TypeManager
    {
        CoFABContext context;
        public TypeManager(CoFABContext context)
        {
            this.context = context;
        }
        public List<Models.Type> GetTypes()
        {
            return context.Types.ToList();
        }
    }
}
