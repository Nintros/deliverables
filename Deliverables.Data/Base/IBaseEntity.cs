using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverables.Data.Base
{
    public interface IBaseEntity
    {
        bool Deleted { get; set; }
    }
}
