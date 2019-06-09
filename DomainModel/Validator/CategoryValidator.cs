// *********************************************************************************
// <copyright file="CategoryValidator.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace DomainModel.Validator
{
    using FluentValidation;
    using log4net;

    /// <summary>
    /// Class Category Validator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{DomainModel.Category}" />
    public class CategoryValidator : AbstractValidator<Category>
    {
        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(CategoryValidator));

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryValidator" /> class.
        /// </summary>
        public CategoryValidator()
        {
            RuleFor(category => category.Name).NotEmpty();
            RuleFor(category => category.Name).Length(4, 50);
        }
    }
}