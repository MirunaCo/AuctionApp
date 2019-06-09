// *********************************************************************************
// <copyright file="ScoreValidator.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace DomainModel.Validator
{
    using FluentValidation;
    using log4net;

    /// <summary>
    /// Class ScoreValidator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{DomainModel.Score}" />
    public class ScoreValidator : AbstractValidator<Score>
    {
        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(ScoreValidator));

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreValidator" /> class.
        /// </summary>
        public ScoreValidator()
        {
            RuleFor(score => score.UserIdTo).NotEmpty();
            RuleFor(score => score.User).SetValidator(new UserValidator());
            RuleFor(score => score.UserIdFrom).NotEmpty();
            RuleFor(score => score.UserFrom).SetValidator(new UserValidator());
            RuleFor(score => score.Points).InclusiveBetween(0, 5);
        }
    }
}
