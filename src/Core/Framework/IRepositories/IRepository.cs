using Framework.QuerySpecificationPattern;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.IRepositories
{
    public interface IRepository<T> : ISaveRepository<T>, IDeleteRepository<T>, IFetchRepository<T> where T : class
    {
        IQueryable<T> FetchWithSpecification(ISpecification<T> spec);

        int RunQuery(string query, params object[] parameters);

        IList<S> RunQuery<S>(string query, params object[] parameters);

        S RunRawQuery<S>(string query, params object[] parameters);

        System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void SaveChanges();

        Task StartTransaction(Func<Task> action);


        IExecutionStrategy CreateExecutionStrategy();
        Task<IDbContextTransaction> GetTransactionAsync();
        IDbContextTransaction GetTransaction();
        void CommitTransaction(IDbContextTransaction transaction);
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        Task TransactionRollbackAsync(IDbContextTransaction transaction);
        void TransactionRollback(IDbContextTransaction transaction);
    }
}
