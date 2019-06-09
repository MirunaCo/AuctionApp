// *********************************************************************************
// <copyright file="ICurrencyServices.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************

namespace ServiceLayer.ServiceInterface
{
    using DomainModel;

    /// <summary>
    /// Interface ICurrencyServices
    /// </summary>
    /// <seealso cref="ServiceLayer.ServiceInterface.IServices{DomainModel.Currency}" />
    /// <seealso cref="AuctionServiceLayer.ServiceInterface.IService{AuctionApplication.DomainModel.Currency}" />
    public interface ICurrencyServices : IServices<Currency>
    {
    }
}
