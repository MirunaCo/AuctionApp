// *********************************************************************************
// <copyright file="BidValidator.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************

namespace DomainModel.Validator
{
    using System;
    using System.Linq;
    using FluentValidation;
    using log4net;

    /// <summary>
    /// Class BidValidator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{DomainModel.Bid}" />
    public class BidValidator : AbstractValidator<Bid>
    {
        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BidValidator));

        /// <summary>
        /// Initializes a new instance of the <see cref="BidValidator" /> class.
        /// </summary>
        public BidValidator()
        {
            RuleFor(bid => bid.Product).SetValidator(new ProductValidator());
            RuleFor(bid => bid.Amount).GreaterThanOrEqualTo(0).WithMessage("Ammount can't be negative");
            RuleFor(bid => bid.Date).GreaterThanOrEqualTo(DateTime.Now.AddSeconds(-1));
            RuleFor(bid => bid.Date).LessThanOrEqualTo(DateTime.Now.AddSeconds(1));
            RuleFor(bid => bid).Must(this.GreaterThenLastestBidAmount).WithMessage("Amount should be greater.");
        }

        /// <summary>
        /// Greater the then last bid amount.
        /// </summary>
        /// <param name="bid">The bid.</param>
        /// <returns>If amount of parameter bid is greater than last bid amount</returns>
        private bool GreaterThenLastestBidAmount(Bid bid)
        {
            this.log.Info("bid greater than latest bid");
            if (bid.Product == null)
            {
                return false;
            }

            if (bid.Product.Bid == null || bid.Product.Bid.Count == 0)
            {
                return bid.Product.Price < bid.Amount;
            }

            var latestBid = bid.Product.Bid.OrderByDescending(b => b.Date).FirstOrDefault(b => !b.Id.Equals(bid.Id));
            if (latestBid == null)
            {
                return false;
            }

            return latestBid.Amount < bid.Amount;
        }
    }
    }
