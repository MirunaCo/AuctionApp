// *********************************************************************************
// <copyright file="CurrencyValidator.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace DomainModel.Validator
{
    using FluentValidation;
    using log4net;

    /// <summary>
    /// Class CurrencyValidator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{DomainModel.Currency}" />
    public class CurrencyValidator : AbstractValidator<Currency>
    {
        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(CurrencyValidator));

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyValidator" /> class.
        /// </summary>
        public CurrencyValidator()
        {
            RuleFor(currency => currency.Name).NotEmpty();
            RuleFor(currency => currency.Name).Length(4, 50);
            RuleFor(currency => currency.Abbreviation).NotEmpty();
            RuleFor(currency => currency.Abbreviation).Length(1, 10);
        }
    }
}
