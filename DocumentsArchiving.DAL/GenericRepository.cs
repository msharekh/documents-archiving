using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsArchiving.DAL
{
    public class GenericRepository<T> : IDisposable where T : class

    {

        #region Declaratioons

        private DocDBEntities _context = null;

        private DbSet<T> table = null;

        public DocDBEntities Context { get { return _context; } }

        #endregion



        #region Constructor

        public GenericRepository()

        {

            this._context = new DocDBEntities();

            table = _context.Set<T>();

        }



        #endregion



        #region CRUD Operations



        #region Get



        /// <summary>

        /// Find object by any filter

        /// </summary>

        /// <param name="filter">Predicate to filter</param>

        /// <returns>First or default value of filtered result</returns>

        public T Find(Expression<Func<T, bool>> filter, List<string> includes = null)

        {

            IQueryable<T> queryable = table;



            if (null != includes)

            {

                foreach (var includeItem in includes)

                {

                    queryable = queryable.Include(includeItem);

                }

            }

            IQueryable<T> result = queryable.Where(filter);



            return result.AsNoTracking().FirstOrDefault();

        }



        /// <summary>

        /// Get all recoreds with no options

        /// </summary>

        /// <returns></returns>

        public IEnumerable<T> GetAll(List<string> includes = null)

        {

            IQueryable<T> queryable = table;



            if (null != includes)

            {

                foreach (var includeItem in includes)

                {

                    queryable = queryable.AsNoTracking().Include(includeItem);

                }

            }

            return queryable.AsNoTracking().ToList();

        }



        public IEnumerable<T> GetAll(List<Expression<Func<T, bool>>> filters)

        {

            if (filters == null || filters.Count <= 0)

            {

                throw new ArgumentException();

            }

            IQueryable<T> result = table.Where(filters[0]);



            #region Filtering

            for (int i = 1; i < filters.Count; i++)

            {

                result = result.Where(filters[i]);

            }

            #endregion



            return result.AsNoTracking().ToList();

        }



        public IEnumerable<T> GetAll(List<Expression<Func<T, bool>>> filters, string OrderByAttrName, bool orderByDesc)

        {

            if (filters == null || filters.Count <= 0)

            {

                throw new ArgumentException();

            }

            IQueryable<T> result = table.Where(filters[0]);



            #region Filtering

            for (int i = 1; i < filters.Count; i++)

            {

                result = result.Where(filters[i]);

            }

            #endregion



            if (string.IsNullOrEmpty(OrderByAttrName))

            {

                return result.AsNoTracking().ToList();

            }



            var order = GetOrderStatement<T>(OrderByAttrName);

            if (orderByDesc)

            {

                return result.AsNoTracking().OrderByDescending(order).ToList();

            }

            else

            {

                return result.AsNoTracking().OrderBy(order).ToList();

            }





        }



        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, List<string> includes = null)

        {

            IQueryable<T> queryable = table;



            if (null != includes)

            {

                foreach (var includeItem in includes)

                {

                    queryable = queryable.Include(includeItem);

                }

            }



            IQueryable<T> result = queryable.Where(filter);



            return result.AsNoTracking().ToList();

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, string OrderByAttrName, bool orderByDesc, List<string> includes = null)

        {

            IQueryable<T> queryable = table;



            if (null != includes)

            {

                foreach (var includeItem in includes)

                {

                    queryable = queryable.Include(includeItem);

                }

            }



            IQueryable<T> result = queryable.Where(filter);

            if (string.IsNullOrEmpty(OrderByAttrName))

            {

                return result.AsNoTracking().ToList();

            }



            var order = GetOrderStatement<T>(OrderByAttrName);

            if (orderByDesc)

            {

                return result.AsNoTracking().OrderByDescending(order).ToList();

            }

            else

            {

                return result.AsNoTracking().OrderBy(order).ToList();

            }

        }



        /// <summary>

        /// Get data from T entity by filters

        /// </summary>

        /// <param name="filters">List of filters</param>

        /// <param name="EnablePaging">True to return paged result</param>

        /// <param name="OrderByAttrName">column name to order by</param>

        /// <param name="orderByDesc">True to order by desc</param>

        /// <param name="pagesCount">Paged result pages count</param>

        /// <param name="pageSize">Number of rows per page</param>

        /// <param name="pageNumber">Page number to display</param>

        /// <returns>IEnumerable<T> ordered result after apllying all filters</returns>

        public IEnumerable<T> GetByFilters(

            List<Expression<Func<T, bool>>> filters, bool EnablePaging, string OrderByAttrName, bool orderByDesc, out int? RowsCount, int? pageSize, int? pageNumber)

        {

            if (filters == null || filters.Count <= 0)

            {

                throw new ArgumentException();

            }

            IQueryable<T> result = table.Where(filters[0]);

            var maxPageSize = 5;

            double pages = 0;

            int skipCount = 0;



            #region Filtering

            for (int i = 1; i < filters.Count; i++)

            {

                result = result.Where(filters[i]);

            }

            #endregion



            if (EnablePaging)

            {

                #region Paging computation

                pageNumber = (pageNumber < 1) ? 1 : pageNumber;



                pageSize = (pageSize > maxPageSize) ? maxPageSize : pageSize;

                pageSize = (pageSize < 1) ? maxPageSize : pageSize;



                int resultCount = result.AsNoTracking().Count();



                pages = (double)resultCount / pageSize.Value;

                pages = (int)Math.Ceiling(pages);



                if (pageNumber > (int)pages)

                {

                    pageNumber = 1;

                }



                skipCount = (pageNumber.Value - 1) * pageSize.Value;

                #endregion



                RowsCount = resultCount;//result.AsNoTracking().Count(); //(int)pages;





                var order = GetOrderStatement<T>(OrderByAttrName);

                if (orderByDesc)

                {

                    return result.AsNoTracking().OrderByDescending(order).Skip(skipCount).Take(pageSize.Value).ToList();

                }

                else

                {

                    return result.AsNoTracking().OrderBy(order).Skip(skipCount).Take(pageSize.Value).ToList();

                }



            }

            else

            {

                RowsCount = 0;

                return result.AsNoTracking().ToList();

            }





        }



        public IEnumerable<T> GetByFilter(

           Expression<Func<T, bool>> filter, bool EnablePaging, string OrderByAttrName, bool orderByDesc, out int? RowsCount, int? pageSize, int? pageNumber)

        {

            List<Expression<Func<T, bool>>> filters = new List<Expression<Func<T, bool>>> { filter };

            return GetByFilters(filters, EnablePaging, OrderByAttrName, orderByDesc, out RowsCount, pageSize, pageNumber);

        }



        public IEnumerable<T> GetByFiltersDataTables(

    List<Expression<Func<T, bool>>> filters, string OrderByAttrName, bool orderByDesc, out int? RowsCount, int? pageSize, int? skipCount, List<string> includes = null)

        {



            IQueryable<T> queryable = table;



            if (null != includes)

            {

                foreach (var includeItem in includes)

                {

                    queryable = queryable.AsNoTracking().Include(includeItem);

                }

            }

            if (filters == null || filters.Count <= 0)

            {

                throw new ArgumentException();

            }

            IQueryable<T> result = queryable.Where(filters[0]);

            //var maxPageSize = 5;



            #region Filtering

            for (int i = 1; i < filters.Count; i++)

            {

                result = result.Where(filters[i]);

            }

            // Chceck sql statement



            #endregion

            RowsCount = result.AsNoTracking().Count(); //(int)pages;



            if (string.IsNullOrEmpty(OrderByAttrName))

            {

                return result.AsNoTracking().ToList();

            }



            var order = GetOrderStatement<T>(OrderByAttrName);

            if (orderByDesc)

            {

                return result.AsNoTracking().OrderByDescending(order).Skip(skipCount.Value).Take(pageSize.Value).ToList();

            }

            else

            {

                return result.AsNoTracking().OrderBy(order).Skip(skipCount.Value).Take(pageSize.Value).ToList();

            }









        }



        #endregion

        public IEnumerable<T> GetByFilterDataTables(

                                                     Expression<Func<T, bool>> filter,

                                                      string OrderByAttrName,

                                                      bool orderByDesc,

                                                      out int? RowsCount,

                                                      int? pageSize,

                                                      int? skipCount,

                                                      List<string> includes = null

                                                    )

        {



            IQueryable<T> queryable = table;



            if (null != includes)

            {

                foreach (var includeItem in includes)

                {

                    queryable = queryable.AsNoTracking().Include(includeItem);

                }

            }



            if (filter == null)

            {

                throw new ArgumentException();

            }



            IQueryable<T> result = queryable.Where(filter).AsNoTracking();

            _context.Database.Log = s => System.Diagnostics.Debug.WriteLine(Environment.NewLine + s + Environment.NewLine);



            //var maxPageSize = 5;





            RowsCount = result.AsNoTracking().Count(); //(int)pages;



            var order = GetOrderStatement<T>(OrderByAttrName);

            if (orderByDesc)

            {

                return result.AsNoTracking().OrderByDescending(order).Skip(skipCount.Value).Take(pageSize.Value).ToList();

            }

            else

            {

                return result.AsNoTracking().OrderBy(order).Skip(skipCount.Value).Take(pageSize.Value).ToList();

            }





        }



        /// <summary>

        /// Add new object

        /// </summary>

        /// <param name="obj">Entity of type T to add</param>

        public void Insert(T obj)

        {

            table.Add(obj);

        }



        /// <summary>

        /// Add new object

        /// </summary>

        /// <param name="obj">Entity of type T to add</param>

        public void BulkInsert(List<T> objects)

        {

            foreach (var obj in objects)

            {

                table.Add(obj);



            }

        }





        /// <summary>

        /// Update existing object

        /// </summary>

        /// <param name="obj">Entity of type T to update</param>

        public void Update(T obj)

        {

            table.Attach(obj);

            _context.Entry(obj).State = EntityState.Modified;

        }

        /// <summary>

        /// Delete object

        /// </summary>

        /// <param name="obj">Entity of type T to delete</param>

        public void Delete(object id)

        {

            T existing = table.Find(id);

            table.Remove(existing);

        }

        /// <summary>

        /// Save changes after insert,update and delete

        /// </summary>

        public void SaveChanges()

        {



            try

            {

                // Your code...

                // Could also be before try if you know the exception occurs in SaveChanges



                _context.SaveChanges();

            }

            catch (DbEntityValidationException e)

            {

                foreach (var eve in e.EntityValidationErrors)

                {

                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",

                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)

                    {

                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",

                            ve.PropertyName, ve.ErrorMessage);

                    }

                }

                throw;

            }

        }



        // Dispose() calls Dispose(true)

        public void Dispose()

        {

            Dispose(true);

            GC.SuppressFinalize(this);

        }



        // The bulk of the clean-up code is implemented in Dispose(bool)

        protected virtual void Dispose(bool disposing)

        {

            if (disposing)

            {

                // free managed resources

                _context.Dispose();

            }



            // free native resources if there are any.

            // ...

        }

        #endregion



        #region Private Methods

        public Func<T, object> GetOrderStatement<T>(string attrName)

        {

            var parameterExpression = Expression.Parameter(typeof(T), attrName);

            var property = Expression.PropertyOrField(parameterExpression, attrName);



            Expression expression = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(expression, parameterExpression).Compile();



        }

        #endregion



    }
}
