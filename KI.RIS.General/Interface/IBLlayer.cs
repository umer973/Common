using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI.RIS.General.Interface
{
    /// <summary>
    /// //////////////not implemented..........................
    /// </summary>
    public interface IBLlayer
    {
        object SelectInitialData();
        object Insert(object data);
        object Update(object data);
        object Delete(object data);
        object Select(short mode, object criteria);
    }
}
