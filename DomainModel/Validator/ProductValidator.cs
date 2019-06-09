// *********************************************************************************
// <copyright file="ProductValidator.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************

namespace DomainModel.Validator
{
    using System;
    using FluentValidation;
    using log4net;

    /// <summary>
    /// Class ProductValidator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{DomainModel.Product}" />
    public class ProductValidator : AbstractValidator<Product>
    {
        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(ProductValidator));

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductValidator" /> class.
        /// </summary>
        public ProductValidator()
        {
            RuleFor(product => product.StartDate).LessThan(product => product.EndDate).WithMessage("Start date should be greater than End date.");
            RuleFor(product => product.StartDate).GreaterThanOrEqualTo(DateTime.Now).WithMessage("Start date can't be in past.");
            RuleFor(product => product.EndDate).GreaterThanOrEqualTo(DateTime.Now).WithMessage("End date can't be in past.");
            RuleFor(product => product.Currency).SetValidator(new CurrencyValidator());
            RuleFor(product => product.Category).SetValidator(new CategoryValidator());
            RuleFor(product => product.Description).NotEmpty();
            RuleFor(product => product.Description).Length(49, 250);
            RuleFor(product => product.Name).NotEmpty();
            RuleFor(product => product.Name).Length(9, 49);
            RuleFor(product => product.Price).GreaterThan(0).WithMessage("Price should be greater then 0.");
        }
    }
}
