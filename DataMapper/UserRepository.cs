// *********************************************************************************
// <copyright file="UserRepository.cs" company="Transilvania University of Brasov">
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
    /// Class UserRepository.
    /// </summary>
    /// <seealso cref="DataMapper.IRepositoryUser" />
    public class UserRepository : IRepositoryUser
    {
        /// <summary>
        /// The database set.
        /// </summary>
        private readonly DbSet<User> databaseSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        public UserRepository()
        {
            using (var context = new Model1Container())
            {
                this.databaseSet = context.Set<User>();
            }
        }

        /// <summary>
        /// This method insert a user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Insert(User user)
        {
            using (var context = new Model1Container())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List User.</returns>
        public List<User> Get(
            Expression<System.Func<User, bool>> filter = null,
            System.Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<User> query = this.databaseSet;
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
        /// <returns>User by id.</returns>
        public User GetByID(int id)
        {
            using (var context = new Model1Container())
            {
                return context.Users.Find(id);
            }
        }

        /// <summary>
        /// This method update a client.
        /// </summary>
        /// <param name="user">The client.</param>
        public void Update(User user)
        {
            using (var context = new Model1Container())
            {
                context.Users.Attach(user);
                context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// This method delete a client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void Delete(User client)
        {
            using (var context = new Model1Container())
            {
                context.Users.Remove(client);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(int id)
        {
            User entityToDelete = this.databaseSet.Find(id);
            this.Delete(entityToDelete);
        }

        /// <summary>
        /// Gets the user with products.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The User</returns>
        public virtual User GetUserWithProducts(int id)
        {
            User returnUser = new User();
            using (var context = new Model1Container())
            {
                User user = this.GetByID(id);
                
                    if (user.Product != null)
                    {
                    returnUser = context.Users.Find(id);
                    }

                return returnUser;
            }
         }
    }
}
