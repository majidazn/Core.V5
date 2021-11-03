using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.AuditBase.Attributes
{
    public class EntityInformationAttribute : Attribute
    {
        public EntityInformationAttribute(string schema, string table, string primaryKey)
        {
            this.Schema = schema;
            this.Table = table;
            this.PrimaryKey = primaryKey;
        }
        public string Schema { get; private set; }

        public string Table { get; private set; }

        public string PrimaryKey { get; private set; }
    }
}
