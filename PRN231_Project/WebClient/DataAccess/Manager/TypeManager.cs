using CoFAB.DataAccess.Models;

namespace CoFAB.DataAccess.Manager
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
