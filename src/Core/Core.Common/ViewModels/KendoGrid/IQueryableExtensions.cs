using Common.ViewModels.KendoGrid;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Common.ViewModels.KendoGrid
{
    public static class IQueryableExtensions
    {
        public static GridDataResult<T> ApplyPaging<T>(this IQueryable<T> query, GridState queryModel)
        {
            if (queryModel == null)
                queryModel = new GridState() { skip = 0, take = 10 };

            var count = query.Count();

            if (queryModel == null || (queryModel.take == 0 && queryModel.skip == 0))
                queryModel = new GridState()
                {
                    skip = 0,
                    take = 10
                };

            query = query.ApplyOrdering(queryModel).Skip(queryModel.skip).Take(queryModel.take);

            var result = new GridDataResult<T>();
            result.total = count;
            result.data = query.ToList();

            return result;
        }

        public async static Task<GridDataResult<T>> ApplyPagingAsync<T>(this IQueryable<T> query, GridState queryModel, int count = 0)
        {
            if (queryModel == null)
                queryModel = new GridState() { skip = 0, take = 10 };

            if (count == 0) count = (await query.CountAsync());

            if (queryModel == null || (queryModel.take == 0 && queryModel.skip == 0))
                queryModel = new GridState()
                {
                    skip = 0,
                    take = 10
                };


            query = query.ApplyOrdering(queryModel).Skip(queryModel.skip).Take(queryModel.take);

            var result = new GridDataResult<T>();
            result.total = count;
            result.data = await query.ToListAsync();

            return result;
        }

        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, GridState model)
        {
            if (model == null || model.sort == null || model.sort.Count == 0)
                return query;

            if (model.sort.Any(a => a.dir == null))
                return query;

            var isFirstTime = true;
            foreach (var sort in model.sort)
            {
                if (string.IsNullOrWhiteSpace(sort.field))
                    continue;

                var propertyInfo = typeof(T).GetProperty(sort.field, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                if (sort.dir == "asc")
                {
                    if (isFirstTime)
                        query = query.OrderBy(propertyInfo.Name);
                    else
                        query = query.ThenBy(propertyInfo.Name);
                }
                else
                {
                    if (isFirstTime)
                        query = query.OrderByDescending(propertyInfo.Name);
                    else
                        query = query.ThenByDescending(propertyInfo.Name);
                }

                isFirstTime = false;
            }

            return query;
        }
    }
}
