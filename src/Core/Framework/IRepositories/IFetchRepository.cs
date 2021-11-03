using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.IRepositories
{
    public interface IFetchRepository<T> where T : class
    {

        IQueryable<T> FetchAll();

        IQueryable<T> FetchMultiWithTracking(Expression<Func<T, bool>> predicate = null);

        IQueryable<T> FetchMulti(Expression<Func<T, bool>> predicate = null);

        Boolean Any(Expression<Func<T, bool>> predicate);

        Task<Boolean> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        T FirstOrDefault(Expression<Func<T, bool>> predicate = null);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default);

        T FirstOrDefaultWithReload(Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefaultWithReloadAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<T> LastOrDefaultWithReloadAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        T LastOrDefault(Expression<Func<T, bool>> predicate);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        int Count(Expression<Func<T, bool>> predicate = null);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default);

        T FetchFirstOrDefaultAsNoTracking(Expression<Func<T, bool>> predicate);

        Task<T> FetchFirstOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        void ExecuteSqlQuery(SqlCommand cmd);

        DataSet ExecuteSqlQueryWithResult(SqlCommand cmd);
    }
}
