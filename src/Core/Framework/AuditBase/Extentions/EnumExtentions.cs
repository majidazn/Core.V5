using Framework.AuditBase.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Framework.AuditBase.Extentions
{
    public static class EnumExtentions
    {
        private static Hashtable _values = new Hashtable();
        public static string GetSchemaValue(this Enum value)
        {
            string result = null;
            Type type = value.GetType();

            if (_values.ContainsKey(value))
                result = (_values[value] as EntityInformationAttribute).Schema;
            else
            {
                FieldInfo field = type.GetField(value.ToString());
                EntityInformationAttribute[] attrs =
                   field.GetCustomAttributes(typeof(EntityInformationAttribute),
                                           false) as EntityInformationAttribute[];
                if (attrs.Length > 0)
                {
                    _values.Add(value, attrs[0]);
                    result = attrs[0].Schema;
                }
            }

            return result;
        }

        public static string GetTableValue(this Enum table)
        {
            string result = null;
            Type type = table.GetType();

            if (_values.ContainsKey(table))
                result = (_values[table] as EntityInformationAttribute).Table;
            else
            {
                FieldInfo field = type.GetField(table.ToString());
                EntityInformationAttribute[] attrs =
                   field.GetCustomAttributes(typeof(EntityInformationAttribute),
                                           false) as EntityInformationAttribute[];
                if (attrs.Length > 0)
                {
                    _values.Add(table, attrs[0]);
                    result = attrs[0].Table;
                }
            }

            return result;
        }

        public static string GetPrimaryKeyValue(this Enum primaryKey)
        {
            string result = null;
            Type type = primaryKey.GetType();

            if (_values.ContainsKey(primaryKey))
                result = (_values[primaryKey] as EntityInformationAttribute).PrimaryKey;
            else
            {
                FieldInfo field = type.GetField(primaryKey.ToString());
                EntityInformationAttribute[] attrs =
                   field.GetCustomAttributes(typeof(EntityInformationAttribute),
                                           false) as EntityInformationAttribute[];
                if (attrs.Length > 0)
                {
                    _values.Add(primaryKey, attrs[0]);
                    result = attrs[0].PrimaryKey;
                }
            }

            return result;
        }
    }
}
