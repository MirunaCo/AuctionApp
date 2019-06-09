// *********************************************************************************
// <copyright file="IScoreServices.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace ServiceLayer.ServiceInterface
{
    using DomainModel;

    /// <summary>
    /// Interface IScoreServices
    /// </summary>
    /// <seealso cref="ServiceLayer.ServiceInterface.IServices{DomainModel.Score}" />
    /// <seealso cref="AuctionServiceLayer.ServiceInterface.IService{AuctionApplication.DomainModel.Score}" />
    public interface IScoreServices : IServices<Score>
    {
    }
}
