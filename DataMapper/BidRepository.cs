// *********************************************************************************
// <copyright file="BidRepository.cs" company="Transilvania University of Brasov">
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
    /// Class BidRepository.
    /// </summary>
    /// <seealso cref="DataMapper.IRepositoryBid" />
    public class BidRepository : IRepositoryBid
    {
        /// <summary>
        /// The database set.
        /// </summary>
        private readonly DbSet<Bid> databaseSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="BidRepository" /> class.
        /// </summary>
        public BidRepository()
        {
            using (var context = new Model1Container())
            {
                this.databaseSet = context.Set<Bid>();
            }
        }

        /// <summary>
        /// This method insert a bid.
        /// </summary>
        /// <param name="bid">The bid.</param>
        public void Insert(Bid bid)
        {
            using (var context = new Model1Container())
            {
                context.Bids.Add(bid);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List bids.</returns>
        public List<Bid> Get(
            Expression<System.Func<Bid, bool>> filter = null,
            System.Func<IQueryable<Bid>, IOrderedQueryable<Bid>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Bid> query = this.databaseSet;
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
        /// <returns>Bid by id.</returns>
        public Bid GetByID(int id)
        {
            using (var context = new Model1Container())
            {
                return context.Bids.Find(id);
            }
        }

        /// <summary>
        /// This method update a bid.
        /// </summary>
        /// <param name="bid">The bid.</param>
        public void Update(Bid bid)
        {
            using (var context = new Model1Container())
            {
                context.Bids.Attach(bid);
                context.Entry(bid).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// This method delete a product.
        /// </summary>
        /// <param name="bid">The bid.</param>
        public void Delete(Bid bid)
        {
            using (var context = new Model1Container())
            {
                context.Bids.Remove(bid);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(int id)
        {
            Bid entityToDelete = this.databaseSet.Find(id);
            this.Delete(entityToDelete);
        }
    }
}