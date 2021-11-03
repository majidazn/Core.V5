using Framework.DomainDrivenDesign.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Reflection;

namespace Framework.Extensions
{
    public static class EntityEntryExtensions
    {
        public static void SetTenantId(this EntityEntry entityEntry, int? tenantId)
        {
            if (entityEntry.State == EntityState.Added && entityEntry.Entity is Entity)
            {
                var tenantObj = entityEntry.Entity.GetType().GetProperties().FirstOrDefault(p => p.Name == "TenantId");
                if (tenantObj != null && tenantObj.PropertyType.IsSubclassOf(typeof(ValueObject)))
                    return;

                if (entityEntry.Metadata.FindProperty("TenantId") != null)
                {
                    var findTenantId = entityEntry.Property("TenantId").CurrentValue?.ToString();
                    if ((string.IsNullOrWhiteSpace(findTenantId) || int.Parse(findTenantId) == 0) && entityEntry.State == EntityState.Added)
                    {
                        entityEntry.Property("TenantId").CurrentValue = tenantId;
                    }
                }
            }
        }

        public static void CleanString(this EntityEntry entityEntry)
        {
            if (entityEntry.Entity != null && (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified))
            {
                var properties = entityEntry.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(entityEntry.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(entityEntry.Entity, newVal, null);
                    }
                }
            }
        }

    }
}