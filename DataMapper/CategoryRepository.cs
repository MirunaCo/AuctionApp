// *********************************************************************************
// <copyright file="CategoryRepository.cs" company="Transilvania University of Brasov">
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
    /// Class CategoryRepository.
    /// </summary>
    /// <seealso cref="DataMapper.IRepositoryCategory" />
    public class CategoryRepository : IRepositoryCategory
    {
        /// <summary>
        /// The database set.
        /// </summary>
        private readonly DbSet<Category> databaseSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository" /> class.
        /// </summary>
        public CategoryRepository()
        {
            using (var context = new Model1Container())
            {
                this.databaseSet = context.Set<Category>();
            }
        }

        /// <summary>
        /// This method insert a category.
        /// </summary>
        /// <param name="category">The category.</param>
        public void Insert(Category category)
        {
            using (var context = new Model1Container())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List category.</returns>
        public List<Category> Get(
            Expression<System.Func<Category, bool>> filter = null,
            System.Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Category> query = this.databaseSet;
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
        /// <returns>Category by id.</returns>
        public Category GetByID(int id)
        {
            using (var context = new Model1Container())
            {
                return context.Categories.Find(id);
            }
        }

        /// <summary>
        /// This method update a category.
        /// </summary>
        /// <param name="category">The category.</param>
        public void Update(Category category)
        {
            using (var context = new Model1Container())
            {
                context.Categories.Attach(category);
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// This method delete a category.
        /// </summary>
        /// <param name="category">The category.</param>
        public void Delete(Category category)
        {
            using (var context = new Model1Container())
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(int id)
        {
            Category entityToDelete = this.databaseSet.Find(id);
            this.Delete(entityToDelete);
        }
    }
}