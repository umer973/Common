using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI.RIS.General.Interface
{
    /// //////////////not implemented..........................
    public interface IDLLayer
    {
        object Insert(object data, IDbTransaction trans);
        object Update(object data, IDbTransaction trans);
        object Delete(object data, IDbTransaction trans);
        object Select(object criteria, IDbConnection cons);
    }
}
