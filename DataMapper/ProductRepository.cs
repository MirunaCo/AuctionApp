// *********************************************************************************
// <copyright file="ProductRepository.cs" company="Transilvania University of Brasov">
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
    /// Class ProductRepository.
    /// </summary>
    /// <seealso cref="DataMapper.IRepositoryProduct" />
    public class ProductRepository : IRepositoryProduct
    {
        /// <summary>
        /// The database set.
        /// </summary>
        private readonly DbSet<Product> databaseSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository" /> class.
        /// </summary>
        public ProductRepository()
        {
            using (var context = new Model1Container())
            {
                this.databaseSet = context.Set<Product>();
            }
        }

        /// <summary>
        /// This method insert a product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Insert(Product product)
        {
            using (var context = new Model1Container())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List products.</returns>
        public List<Product> Get(
            Expression<System.Func<Product, bool>> filter = null,
            System.Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Product> query = this.databaseSet;
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
        /// <returns>Product by id.</returns>
        public Product GetByID(int id)
        {
            using (var context = new Model1Container())
            {
                return context.Products.Find(id);
            }
        }

        /// <summary>
        /// This method update a product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Update(Product product)
        {
            using (var context = new Model1Container())
            {
                context.Products.Attach(product);
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// This method delete a product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Delete(Product product)
        {
            using (var context = new Model1Container())
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(int id)
        {
            Product entityToDelete = this.databaseSet.Find(id);
            this.Delete(entityToDelete);
        }

        /// <summary>
        /// Gets all active products.
        /// </summary>
        /// <returns>IEnumerable Product</returns>
        public virtual List<Product> GetAllActiveProducts()
        {
                using (var context = new Model1Container())
                {
                    List<Product> returnProducts = new List<Product>();
                    List<Product> products = this.Get();
                    foreach (Product prod in products)
                    {
                        if (prod.IsActive && prod.EndDate < DateTime.Now)
                        {
                            returnProducts.Add(context.Products.Find(prod.Id));
                        }
                    }

                    return returnProducts;
                }
        }

        /// <summary>
        /// Gets all except product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>IEnumerable Product</returns>
        public virtual List<Product> GetAllExceptProductId(int productId)
        {
            using (var context = new Model1Container())
            {
                List<Product> returnProducts = new List<Product>();
                List<Product> products = this.Get();
                foreach (Product prod in products)
                {
                    if (!productId.Equals(prod.Id))
                    {
                        returnProducts.Add(context.Products.Find(prod.Id));
                    }
                }

                return returnProducts;
            }
        }
    }
}
