// *********************************************************************************
// <copyright file="IProductServices.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************

namespace ServiceLayer.ServiceInterface
{
    using DomainModel;

    /// <summary>
    /// Interface IProductServices
    /// </summary>
    /// <seealso cref="ServiceLayer.ServiceInterface.IServices{DomainModel.Product}" />
    public interface IProductServices : IServices<Product>
    {
        /// <summary>
        /// Finalizes the product.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="product">The product.</param>
        void FinalizeProduct(User user, Product product);

        /// <summary>
        /// Updates the products status.
        /// </summary>
        void UpdateProductsStatus();
    }
}
