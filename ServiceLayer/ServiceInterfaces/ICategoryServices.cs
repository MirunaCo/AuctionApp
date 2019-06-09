// *********************************************************************************
// <copyright file="ICategoryServices.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************

namespace ServiceLayer.ServiceInterface
{
    using DomainModel;

    /// <summary>
    /// Interface ICategoryServices
    /// </summary>
    /// <seealso cref="ServiceLayer.ServiceInterface.IServices{DomainModel.Category}" />
    /// <seealso cref="AuctionServiceLayer.ServiceInterface.IService{AuctionApplication.DomainModel.Category}" />
    public interface ICategoryServices : IServices<Category>
    {
    }
}
