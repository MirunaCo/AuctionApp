// *********************************************************************************
// <copyright file="CurrencyServices.cs" company="Transilvania University of Brasov">
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
    /// Class CurrencyServices
    /// </summary>
    /// <seealso cref="ServiceLayer.ServiceInterface.ICurrencyServices" />
    /// <seealso cref="AuctionServiceLayer.ServiceInterface.IService{AuctionServiceLayer.Service.CurrencyService}" />
    public class CurrencyServices : ICurrencyServices
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepositoryCurrency repository;

        /// <summary>
        /// The validator
        /// </summary>
        private readonly CurrencyValidator validator;

        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(CurrencyServices));

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyServices" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CurrencyServices(IRepositoryCurrency repository)
        {
            this.repository = repository;
            this.validator = new CurrencyValidator();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            this.log.Info("delete a currency");
            this.repository.Delete(id);
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public void Delete(Currency entityToDelete)
        {
            this.log.Info("delete a currency");
            this.repository.Delete(entityToDelete);
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IEnumerable TEntity.</returns>
        public IEnumerable<Currency> Get(Expression<Func<Currency, bool>> filter = null, Func<IQueryable<Currency>, IOrderedQueryable<Currency>> orderBy = null, string includeProperties = "")
        {
            this.log.Info("get a list of currencies");
            return this.repository.Get(filter, orderBy, includeProperties);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T Entity.</returns>
        public Currency GetByID(int id)
        {
            this.log.Info("get a currency");
            return this.repository.GetByID(id);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="InvalidCurrencyException">If entity is not valid</exception>
        public void Insert(Currency entity)
        {
            this.log.Info("insert a currency");
            bool validationSucceeded = this.validator.Validate(entity).IsValid;

            if (validationSucceeded)
            {
                this.repository.Insert(entity);
            }
            else
            {
                throw new InvalidCurrencyException();
            }
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <exception cref="InvalidCurrencyException">If entity is not valid</exception>
        public void Update(Currency entityToUpdate)
        {
            this.log.Info("update a currency");
            bool validationSucceeded = this.validator.Validate(entityToUpdate).IsValid;

            if (validationSucceeded)
            {
                this.repository.Update(entityToUpdate);
            }
            else
            {
                throw new InvalidCurrencyException();
            }
        }
    }
}
