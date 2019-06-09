// *********************************************************************************
// <copyright file="CategoryServices.cs" company="Transilvania University of Brasov">
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
    /// Class CategoryServices
    /// </summary>
    /// <seealso cref="ServiceLayer.ServiceInterface.ICategoryServices" />
    /// <seealso cref="AuctionServiceLayer.ServiceInterface.IService{AuctionApplication.DomainModel.Category}" />
    public class CategoryServices : ICategoryServices
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepositoryCategory repository;

        /// <summary>
        /// The validator
        /// </summary>
        private readonly CategoryValidator validator;

        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(CategoryServices));

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryServices" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CategoryServices(IRepositoryCategory repository)
        {
            this.repository = repository;
            this.validator = new CategoryValidator();
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public void Delete(Category entityToDelete)
        {
            this.log.Info("delete the category");
            this.repository.Delete(entityToDelete);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            this.log.Info("delete the category");
            this.repository.Delete(id);
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IEnumerable TEntity.</returns>
        public IEnumerable<Category> Get(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "")
        {
            this.log.Info("get the categories");
            return this.repository.Get(filter, orderBy, includeProperties);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T Entity.</returns>
        public Category GetByID(int id)
        {
            this.log.Info("get the category");
            return this.repository.GetByID(id);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="InvalidCategoryException">If entity is not valid</exception>
        public void Insert(Category entity)
        {
            this.log.Info("insert a category");
            bool validationSucceeded = this.validator.Validate(entity).IsValid;

            if (validationSucceeded)
            {
                this.repository.Insert(entity);
            }
            else
            {
                throw new InvalidCategoryException();
            }
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <exception cref="InvalidCategoryException">If entity is not valid</exception>
        public void Update(Category entityToUpdate)
        {
            this.log.Info("update a category");
            bool validationSucceeded = this.validator.Validate(entityToUpdate).IsValid;

            if (validationSucceeded)
            {
                this.repository.Update(entityToUpdate);
            }
            else
            {
                throw new InvalidCategoryException();
            }
        }
    }
}
