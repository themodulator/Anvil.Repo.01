using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Specialized;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anvil.Mapping
{
    public interface IMappableObject
    {
        string FieldName { get; set; }
    }
}
