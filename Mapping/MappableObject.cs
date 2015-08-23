using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Specialized;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace Anvil.Mapping
{
    public abstract class MappableObject<TTarget> 
    {
        #region Constructors

        public MappableObject(TTarget target)
        {
            this.Target = target;
        }

        #endregion

        #region Properties

        public TTarget Target { get; set; }

        #endregion


        #region Helpers

        private PropertyInfo[] GetMappableProperties()
        {
            PropertyInfo[] pp = this.GetType().GetProperties()
                .Where(x => x.GetCustomAttributes(false)
                    .Any(a => a.GetType() == typeof(MapToFieldAttribute))).ToArray();
            return pp;
        }

        #endregion

        #region Convert

        public void FillTarget()
        {
            PropertyInfo[] pp = GetMappableProperties();
            foreach(PropertyInfo p in pp)
            {

                object v = p.GetValue(this, null);

                MapToFieldAttribute a = (MapToFieldAttribute)p.GetCustomAttribute(typeof(MapToFieldAttribute));

                PropertyInfo tp = Target.GetType().GetProperty(a.FieldName);

                if(p.PropertyType == tp.PropertyType)
                    tp.SetValue(Target, v, null);
                else
                {
                    if (tp.PropertyType.IsEnum)
                    {
                        if (p.PropertyType == typeof(string))
                            tp.SetValue(Target, System.Enum.Parse(a.EnumType, a.EnumMap[v.ToString()]), null);
                        else
                            tp.SetValue(Target, System.Enum.ToObject(a.EnumType, v), null);
                    }
                    else
                    {
                        tp.SetValue(Target, Convert.ChangeType(v, tp.PropertyType), null);
                    }
                }
            }
        }



        #endregion



    }
}
