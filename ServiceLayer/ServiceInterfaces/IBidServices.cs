// *********************************************************************************
// <copyright file="IBidServices.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace ServiceLayer.ServiceInterface
{
    using DomainModel;

    /// <summary>
    /// Interface IBidServices
    /// </summary>
    /// <seealso cref="ServiceLayer.ServiceInterface.IServices{DomainModel.Bid}" />
    /// <seealso cref="AuctionServiceLayer.ServiceInterface.IService{AuctionApplication.DomainModel.Bid}" />
    public interface IBidServices : IServices<Bid>
    {
    }
}
