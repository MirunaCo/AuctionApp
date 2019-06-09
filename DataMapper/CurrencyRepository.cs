// *********************************************************************************
// <copyright file="CurrencyRepository.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace DataMapper
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using DomainModel;

    /// <summary>
    /// Class CurrencyRepository.
    /// </summary>
    /// <seealso cref="DataMapper.IRepositoryCurrency" />
    public class CurrencyRepository : IRepositoryCurrency
    {
        /// <summary>
        /// The database set.
        /// </summary>
        private readonly DbSet<Currency> databaseSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyRepository" /> class.
        /// </summary>
        public CurrencyRepository()
        {
            using (var context = new Model1Container())
            {
                this.databaseSet = context.Set<Currency>();
            }
        }

        /// <summary>
        /// This method insert a currency.
        /// </summary>
        /// <param name="currency">The currency.</param>
        public void Insert(Currency currency)
        {
            using (var context = new Model1Container())
            {
                context.Currencies.Add(currency);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List currencies.</returns>
        public List<Currency> Get(
            Expression<System.Func<Currency, bool>> filter = null,
            System.Func<IQueryable<Currency>, IOrderedQueryable<Currency>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Currency> query = this.databaseSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Currency by id.</returns>
        public Currency GetByID(int id)
        {
            using (var context = new Model1Container())
            {
                return context.Currencies.Find(id);
            }
        }

        /// <summary>
        /// This method update a currency.
        /// </summary>
        /// <param name="currency">The currency.</param>
        public void Update(Currency currency)
        {
            using (var context = new Model1Container())
            {
                context.Currencies.Attach(currency);
                context.Entry(currency).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// This method delete a currency.
        /// </summary>
        /// <param name="currency">The currency.</param>
        public void Delete(Currency currency)
        {
            using (var context = new Model1Container())
            {
                context.Currencies.Remove(currency);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(int id)
        {
            Currency entityToDelete = this.databaseSet.Find(id);
            this.Delete(entityToDelete);
        }
    }
}