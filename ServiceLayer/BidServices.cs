// *********************************************************************************
// <copyright file="BidServices.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************

namespace ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DataMapper;
    using DomainModel;
    using DomainModel.Validator;
    using log4net;
    using ServiceInterface;
    using ServiceLayer.Exception;

    /// <summary>
    /// Class BidServices
    /// </summary>
    /// <seealso cref="ServiceLayer.ServiceInterface.IBidServices" />
    public class BidServices : IBidServices
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepositoryBid repository;

        /// <summary>
        /// The validator
        /// </summary>
        private readonly BidValidator validator = new BidValidator();

        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BidServices));

        /// <summary>
        /// Initializes a new instance of the <see cref="BidServices" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BidServices(IRepositoryBid repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public void Delete(Bid entityToDelete)
        {
            this.log.Info("delete bid " + entityToDelete.Id.ToString());
            this.repository.Delete(entityToDelete);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            this.log.Info("delete bid with id " + id.ToString());
            this.repository.Delete(id);
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IEnumerable TEntity.</returns>
        public IEnumerable<Bid> Get(Expression<Func<Bid, bool>> filter = null, Func<IQueryable<Bid>, IOrderedQueryable<Bid>> orderBy = null, string includeProperties = "")
        {
            this.log.Info("get list of bids ");
            return this.repository.Get(filter, orderBy, includeProperties);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T Entity.</returns>
        public Bid GetByID(int id)
        {
            this.log.Info("get bid with id  " + id.ToString());
            return this.repository.GetByID(id);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="InvalidBidException">invalid bid</exception>
        /// <exception cref="AuctionServiceLayer.Exception.InvalidBidException">If entity is not valid</exception>
        public void Insert(Bid entity)
        {
            this.log.Info("insert bid " + entity.Id.ToString());
            var result = this.validator.Validate(entity);
            bool validationSucceeded = result.IsValid;

            if (validationSucceeded)
            {
                this.repository.Insert(entity);
            }
            else
            {
                throw new InvalidBidException();
            }
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <exception cref="InvalidBidException">invalid bid</exception>
        /// <exception cref="AuctionServiceLayer.Exception.InvalidBidException">If entity is not valid</exception>
        public void Update(Bid entityToUpdate)
        {
            this.log.Info("update bid " + entityToUpdate.Id.ToString());
            bool validationSucceeded = this.validator.Validate(entityToUpdate).IsValid;

            if (validationSucceeded)
            {
                this.repository.Update(entityToUpdate);
            }
            else
            {
                throw new InvalidBidException();
            }
        }
    }
}