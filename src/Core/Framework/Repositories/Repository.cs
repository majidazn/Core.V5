using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Z.EntityFramework.Plus;
using System.Threading.Tasks;
using System.Threading;
using Framework.IRepositories;
using Framework.QuerySpecificationPattern;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Http;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace Framework.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region [Eventing Section]
        IRepository<T> implementation;

        #region [Events]
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> BeforeSavingRecord;
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> SavingRecord;
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> RecordSaved;
        public virtual event System.EventHandler<EntityDeletingEventArgs<T>> BeforeDeletingRecord;
        public virtual event System.EventHandler<EntityDeletingEventArgs<T>> DeletingRecord;
        public virtual event System.EventHandler<EntityDeletingEventArgs<T>> RecordDeleted;
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> UpdatingRecord;
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> RecordUpdated;
        #endregion

        public void PopulateEvents(IRepository<T> _implementation)
        {
            implementation = _implementation;

            implementation.BeforeSavingRecord += new EventHandler<EntitySavingEventArgs<T>>(this.OnBeforeSavingRecord);
            implementation.SavingRecord += new EventHandler<EntitySavingEventArgs<T>>(this.OnSavingRecord);
            implementation.RecordSaved += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnRecordSaved);


            implementation.BeforeDeletingRecord += new System.EventHandler<EntityDeletingEventArgs<T>>(this.OnBeforeDeletingRecord);
            implementation.DeletingRecord += new System.EventHandler<EntityDeletingEventArgs<T>>(this.OnDeletingRecord);
            implementation.RecordDeleted += new System.EventHandler<EntityDeletingEventArgs<T>>(this.OnRecordDeleted);


            implementation.UpdatingRecord += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnUpdatingRecord);
            implementation.RecordUpdated += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnRecordUpdated);


        }

        #region [Virtual Mothods]
        public virtual void OnBeforeSavingRecord(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnSavingRecord(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnRecordSaved(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnBeforeDeletingRecord(object sender, EntityDeletingEventArgs<T> e)
        {
        }
        public virtual void OnDeletingRecord(object sender, EntityDeletingEventArgs<T> e)
        {
        }
        public virtual void OnRecordDeleted(object sender, EntityDeletingEventArgs<T> e)
        {
        }
        public virtual void OnUpdatingRecord(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnRecordUpdated(object sender, EntitySavingEventArgs<T> e)
        {
        }
        #endregion

        #endregion
        protected readonly DbContext _dbContext;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private int _tenantId;
        public Repository(DbContext dbContext, IHttpContextAccessor httpContextAccessor = null)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;

        }

        public virtual void Create(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            //System.Diagnostics.Debugger.Break();

            SetTenantId(item);

            if (BeforeSavingRecord != null)
                BeforeSavingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });
            _dbContext.Set<T>().Add(item);

            if (SavingRecord != null)
                SavingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });


            this.SaveChanges();

            if (RecordSaved != null)
                RecordSaved.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });
        }

        public virtual async Task<T> CreateAsync(T item, CancellationToken cancellationToken = default)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            SetTenantId(item);

            if (BeforeSavingRecord != null)
                BeforeSavingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });
            await _dbContext.Set<T>().AddAsync(item, cancellationToken);

            if (SavingRecord != null)
                SavingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            await this.SaveChangesAsync();
            //await _dbContext.SaveChangesAsync();

            if (RecordSaved != null)
                RecordSaved.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            return item;

        }

        public virtual async Task<T> CreateAsyncUoW(T item, CancellationToken cancellationToken = default)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            SetTenantId(item);

            await _dbContext.Set<T>().AddAsync(item);

            return item;
        }

        /// <summary>
        /// It doesn't support Creating Events. Instead use Create(T model)
        /// </summary>
        /// <param name="items"></param>
        public virtual void CreateMulti(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("item");

            foreach (var item in items)
            {
                SetTenantId(item);
            }

            _dbContext.Set<T>().AddRange(items);
            this.SaveChanges(); ;
        }

        public virtual async Task<bool> CreateMultiAsync(IEnumerable<T> items, CancellationToken cancellationToken = default)
        {
            if (items == null)
                throw new ArgumentNullException("item");

            if (!items.Any())
                throw new ArgumentNullException("item");

            foreach (var item in items)
            {
                SetTenantId(item);
            }

            await _dbContext.Set<T>().AddRangeAsync(items, cancellationToken);
            await this.SaveChangesAsync();

            return true;
        }

        public virtual async Task<bool> CreateMultiAsyncUoW(IEnumerable<T> items, CancellationToken cancellationToken = default)
        {
            if (items == null)
                throw new ArgumentNullException("item");

            if (!items.Any())
                throw new ArgumentNullException("item");

            foreach (var item in items)
            {
                SetTenantId(item);
            }

            await _dbContext.Set<T>().AddRangeAsync(items, cancellationToken);

            return true;
        }

        public virtual void Update(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Entry(item).State = EntityState.Modified;
            if (UpdatingRecord != null)
                UpdatingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            this.SaveChanges(); ;
            if (RecordUpdated != null)
                RecordUpdated.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

        }
        public virtual async Task<T> UpdateAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Entry(item).State = EntityState.Modified;
            if (UpdatingRecord != null)
                UpdatingRecord.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            await this.SaveChangesAsync();
            if (RecordUpdated != null)
                RecordUpdated.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            return item;
        }

        public virtual T UpdateUoW(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            // _dbContext.Set<T>().Add(item);
            _dbContext.Set<T>().Update(item);

            return item;
        }
        public virtual void SaveChanges()
        {

            try
            {

                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
            }

        }



        public virtual async System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //System.Diagnostics.Debugger.Break();
            //SetTenantId();

            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// It doesn't support deleting events. Instead Use Delete(T item)
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            _dbContext.Set<T>().Where(predicate).Delete();
            this.SaveChanges(); ;
        }
        public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().Where(predicate).DeleteAsync(cancellationToken);
            return await this.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsyncUoW(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().Where(predicate).DeleteAsync(cancellationToken);
        }

        public virtual bool DeleteUoW(T item)
        {
            _dbContext.Set<T>().Remove(item);
            return true;
        }

        public virtual int DeleteUoW(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).Delete();
            //return true;
        }

        public virtual void Delete(T item)
        {
            if (BeforeDeletingRecord != null)
                BeforeDeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });

            _dbContext.Set<T>().Attach(item);
            _dbContext.Set<T>().Remove(item);

            if (DeletingRecord != null)
                DeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
            this.SaveChanges(); ;

            if (BeforeDeletingRecord != null)
                BeforeDeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
        }
        public virtual async void DeleteAsync(T item, CancellationToken cancellationToken = default)
        {
            if (BeforeDeletingRecord != null)
                BeforeDeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });

            _dbContext.Set<T>().Attach(item);
            _dbContext.Set<T>().Remove(item);

            if (DeletingRecord != null)
                DeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
            await this.SaveChangesAsync();

            if (BeforeDeletingRecord != null)
                BeforeDeletingRecord.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
        }
        public virtual void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var item in entities)
            {
                _dbContext.Entry(item).State = EntityState.Deleted;
            }
            this.SaveChanges(); ;

        }
        public virtual async void DeleteAsync(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var item in entities)
            {
                _dbContext.Entry(item).State = EntityState.Deleted;
            }
            await this.SaveChangesAsync();
        }
        public virtual IQueryable<T> FetchAll()
        {
            return _dbContext.Set<T>().AsNoTracking().AsQueryable();
        }
        public virtual IQueryable<T> FetchMulti(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Set<T>().AsNoTracking() :
                 _dbContext.Set<T>().AsNoTracking().Where(predicate);
        }

        public virtual IQueryable<T> FetchMultiWithTracking(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Set<T>() :
                 _dbContext.Set<T>().Where(predicate);
        }

        public void RemoveEntitiesUoW<S>(IEnumerable<S> entities) where S : class
        {
            foreach (var entity in entities)
            {
                _dbContext.Remove<S>(entity);
            }

        }

        public virtual Boolean Any(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().AsNoTracking().Any(predicate);
        }

        public virtual async Task<Boolean> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().AsNoTracking().AnyAsync(predicate, cancellationToken);
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Set<T>().FirstOrDefault() : _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            return predicate == null ? await _dbContext.Set<T>().FirstOrDefaultAsync() : await _dbContext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual T FirstOrDefaultWithReload(Expression<Func<T, bool>> predicate)
        {
            var entity = _dbContext.Set<T>().FirstOrDefault(predicate);
            if (entity == null)
                return default(T);
            _dbContext.Entry(entity).Reload();
            return entity;
        }

        public virtual async Task<T> LastOrDefaultWithReloadAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Set<T>().LastOrDefaultAsync(predicate, cancellationToken);
            if (entity == null)
                return default(T);
            _dbContext.Entry(entity).Reload();
            return entity;
        }

        public virtual T LastOrDefault(Expression<Func<T, bool>> predicate)
        {
            return predicate == null ? _dbContext.Set<T>().LastOrDefault() : _dbContext.Set<T>().LastOrDefault(predicate);
        }

        public virtual async Task<T> FirstOrDefaultWithReloadAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
            if (entity == null)
                return default(T);
            _dbContext.Entry(entity).Reload();
            return entity;
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().SingleOrDefault(predicate);
        }

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return predicate == null ? await _dbContext.Set<T>().SingleOrDefaultAsync() : await _dbContext.Set<T>().SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual int Count(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ?
            _dbContext.Set<T>().Count() :
            _dbContext.Set<T>().Count(predicate);

        }
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            return predicate == null ? await _dbContext.Set<T>().CountAsync() : await _dbContext.Set<T>().CountAsync(predicate, cancellationToken);
        }
        public virtual int BulkUpdate(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate)
        {
            return _dbContext.Set<T>().Where(predicate).Update(updatePredicate);
        }
        public virtual async Task<int> BulkUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().Where(predicate).UpdateAsync(updatePredicate, cancellationToken);
        }
        public void ExecuteSqlQuery(SqlCommand sqlCommand)
        {
            var entityConnection = _dbContext.Database.GetDbConnection().ConnectionString;
            using (SqlConnection con = new SqlConnection(entityConnection))
            {
                using (SqlCommand cmd = sqlCommand)
                {
                    try
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();

                        if (con != null && con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }
            }
        }
        public DataSet ExecuteSqlQueryWithResult(SqlCommand sqlCommand)
        {
            DataSet ds = new DataSet();
            var entityConnection = _dbContext.Database.GetDbConnection().ConnectionString;
            using (SqlConnection con = new SqlConnection(entityConnection))
            {
                using (SqlCommand cmd = sqlCommand)
                {
                    try
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        if (con != null && con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (con != null && con.State == System.Data.ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }
            }
            return ds;
        }
        public T FetchFirstOrDefaultAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().AsNoTracking().FirstOrDefault(predicate);
        }

        public async Task<T> FetchFirstOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public void UpdateWithAttachUoW(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Set<T>().Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
        }
        public void UpdateWithAttach(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Set<T>().Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
            this.SaveChanges(); ;
        }

        public async void UpdateWithAttachAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Set<T>().Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
            await this.SaveChangesAsync();
        }

        public int RunQuery(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<S> RunQuery<S>(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public S RunRawQuery<S>(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }



        public Task<PropertyValues> GetOriginalValues(T entity)
        {
            return _dbContext.Entry<T>(entity).GetDatabaseValuesAsync();
        }

        public IQueryable<T> FetchWithSpecification(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult
                            .Where(spec.Criteria)
                            .AsQueryable();
        }





        public async Task StartTransaction(Func<Task> action)
        {
            var strategy = CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                // Achieving atomicity between original Catalog database operation and the
                // IntegrationEventLog thanks to a local transaction

                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    await action();
                    transaction.Commit();
                }

            });


            //using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
            //    try
            //    {
            //        await action();
            //        transaction.Complete();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception(ex.Message, ex);
            //    }
            //    finally
            //    {
            //        transaction.Dispose();
            //    }
            //}
        }
        private int GetTenantId()
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any(x => x.Type == "TenantId"))
            {
                var tenant = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "TenantId").FirstOrDefault().Value;

                if (!string.IsNullOrEmpty(tenant))
                {
                    return int.Parse(tenant);
                }
            }

            return 0;
        }
        private void SetTenantId()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
                return;

            var entities = _dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            if (_httpContextAccessor.HttpContext.User.Claims.Count() > 0)
            {
                foreach (var entity in entities)
                {
                    foreach (var member in entity.Entity.GetType().GetMembers())
                    {
                        if (member.Name == "TenantId" &&
                         entity.Entity.GetType()?.GetProperty("TenantId")?.GetValue(entity.Entity)?.ToString() == string.Empty ||
                         entity.Entity.GetType()?.GetProperty("TenantId")?.GetValue(entity.Entity)?.ToString() == "0")
                        {
                            entity.Entity.GetType().GetProperty("TenantId").SetValue(entity.Entity, GetTenantId());
                        }
                    }
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Claims Empty");
            }
        }

        private void SetTenantId<T>(T entity)
        {
            var tenantObj = entity.GetType().GetProperties().FirstOrDefault(p => p.Name == "TenantId");
            if (tenantObj != null && tenantObj.PropertyType.IsSubclassOf(typeof(ValueObject)))
                return;

            if (!entity.GetType().IsSubclassOf(typeof(Entity)))
                return;

            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
                return;

            const string tenant = "TenantId";

            if (_httpContextAccessor.HttpContext.User.Claims.Count() > 0)
            {
                var tenantProperty = entity.GetType().GetMembers().FirstOrDefault(q => q.Name == tenant);
                if (tenantProperty == null)
                    return;

                var tenantMember = entity.GetType().GetProperty("TenantId");
                if (string.IsNullOrWhiteSpace(tenantMember?.GetValue(entity).ToString()) || tenantMember?.GetValue(entity).ToString() == "0")
                {

                    entity.GetType().GetProperty("TenantId").SetValue(entity, GetTenantId());
                }


                //entity.GetType().GetProperty(tenant).SetValue(entity, GetTenantId());
            }
            else
            {
                throw new UnauthorizedAccessException("Claims Empty");
            }
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _dbContext.Database.CreateExecutionStrategy();
        }
        public async Task<IDbContextTransaction> GetTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
        public IDbContextTransaction GetTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }
        public void CommitTransaction(IDbContextTransaction transaction)
        {
            transaction.Commit();
        }
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            await transaction.CommitAsync();
        }

        public async Task TransactionRollbackAsync(IDbContextTransaction transaction)
        {
            await transaction.RollbackAsync();
        }
        public void TransactionRollback(IDbContextTransaction transaction)
        {
            transaction.Rollback();
        }
    }
}
