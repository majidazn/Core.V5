using Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.IRepositories
{
    public interface IDeleteRepository<T> where T : class
    {

        event System.EventHandler<EntityDeletingEventArgs<T>> BeforeDeletingRecord;

        event System.EventHandler<EntityDeletingEventArgs<T>> DeletingRecord;

        event System.EventHandler<EntityDeletingEventArgs<T>> RecordDeleted;


        bool DeleteUoW(T item);
        void Delete(T item);
        void DeleteAsync(T item, CancellationToken cancellationToken = default);
        void Delete(IEnumerable<T> entities);
        void DeleteAsync(IEnumerable<T> entities);
        Task<int> DeleteAsyncUoW(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        void Delete(Expression<Func<T, bool>> predicate);
        Task<int> DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        void RemoveEntitiesUoW<S>(IEnumerable<S> entities) where S : class;
        int DeleteUoW(Expression<Func<T, bool>> predicate);
    }
}
