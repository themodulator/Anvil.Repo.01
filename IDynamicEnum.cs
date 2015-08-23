using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Specialized;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil
{
    public interface IDynamicEnum
    {
        string Caption { get; set; }

        long BinaryValue { get; set; }

        string LiteralName { get; set; }
    }
}
