// *********************************************************************************
// <copyright file="IUserServices.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace ServiceLayer.ServiceInterface
{
    using DomainModel;

    /// <summary>
    /// Interface IUserServices
    /// </summary>
    /// <seealso cref="ServiceLayer.ServiceInterface.IServices{DomainModel.User}" />
    /// <seealso cref="ServiceLayer.ServiceInterface.IService{AuctionApplication.DomainModel.User}" />
    public interface IUserServices : IServices<User>
    {
    }
}
