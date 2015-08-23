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
    public class MapToFieldAttribute : Attribute, IMappableObject
    {
        public MapToFieldAttribute(string fieldName)
        {
            this.FieldName = fieldName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="enumType"></param>
        /// <param name="enumMap">(ie. F=Female,M=Male)</param>
        public MapToFieldAttribute(string fieldName, System.Type enumType, string enumMap)
        {
            this.FieldName = fieldName;
            ParseEnumMap(enumMap);
            EnumType = enumType;
        }

        public string FieldName { get; set; }

        /// <summary>
        /// Maps Enum value (ie. F=Female,M=Male)
        /// </summary>
        public Dictionary<string, string> EnumMap { get; set; }

            public Type EnumType { get; set; }

        public void ParseEnumMap(string enumMap)
        {
            string[] maps = enumMap.Split(',');
            KeyValuePair<string, string>[] prs = maps.Select(x => new KeyValuePair<string, string>(x.Split('=')[0], x.Split('=')[1])).ToArray();
            Dictionary<string, string> parsed = new Dictionary<string,string>();
            foreach(KeyValuePair<string, string> pr in prs)
            {
                parsed.Add(pr.Key, pr.Value);
            }
            EnumMap = parsed;
        }
        

    }
 }
