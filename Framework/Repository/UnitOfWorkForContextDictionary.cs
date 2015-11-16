using System.Collections.Generic;

namespace Repository
{
    public class UnitOfWorkForContextDictionary : Dictionary<string, object>, IUnitOfWorkForContextDictionary
    {
    }
}
