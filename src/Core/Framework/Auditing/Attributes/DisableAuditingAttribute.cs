using System;

namespace Framework.Auditing.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisableAuditingAttribute : Attribute
    {
    }
}
