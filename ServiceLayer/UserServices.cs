// *********************************************************************************
// <copyright file="UserServices.cs" company="Transilvania University of Brasov">
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
    using ServiceLayer.Exception;

    /// <summary>
    /// Class UserServices.cs
    /// </summary>
    public class UserServices
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepositoryUser repository;

        /// <summary>
        /// The validator
        /// </summary>
        private readonly UserValidator validator;

        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(UserServices));

        /// <summary>
        /// Initializes a new instance of the <see cref="UserServices" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserServices(IRepositoryUser repository)
        {
            this.repository = repository;
            this.validator = new UserValidator();
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public void Delete(User entityToDelete)
        {
            this.log.Info("delete a user");
            this.repository.Delete(entityToDelete);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            this.log.Info("delete a user by id");
            this.repository.Delete(id);
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IEnumerable TEntity.</returns>
        public IEnumerable<User> Get(Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "")
        {
            this.log.Info("get a list of users");
            return this.repository.Get(filter, orderBy, includeProperties);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T Entity.</returns>
        public User GetByID(int id)
        {
            this.log.Info("get a user");
            return this.repository.GetByID(id);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ServiceLayer.Exception.InvalidUserException">If entity is not valid</exception>
        /// <exception cref="InvalidUserException">If entity is not valid</exception>
        public void Insert(User entity)
        {
            this.log.Info("insert a user");
            bool validationSucceeded = this.validator.Validate(entity).IsValid;

            if (validationSucceeded)
            {
                this.repository.Insert(entity);
            }
            else
            {
                throw new InvalidUserException();
            }
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <exception cref="ServiceLayer.Exception.InvalidUserException">If entity is not valid</exception>
        /// <exception cref="InvalidUserException">If entity is not valid</exception>
        public void Update(User entityToUpdate)
        {
            this.log.Info("update a user");
            bool validationSucceeded = this.validator.Validate(entityToUpdate).IsValid;

            if (validationSucceeded)
            {
                this.repository.Update(entityToUpdate);
            }
            else
            {
                throw new InvalidUserException();
            }
        }
    }
}
