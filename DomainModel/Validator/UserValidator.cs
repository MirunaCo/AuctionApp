// *********************************************************************************
// <copyright file="UserValidator.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************
namespace DomainModel.Validator
{
    using System.Net.Mail;
    using FluentValidation;
    using log4net;

    /// <summary>
    /// Class UserValidator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{DomainModel.User}" />
    public class UserValidator : AbstractValidator<User>
    {
        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(UserValidator));

        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidator" /> class.
        /// </summary>
        public UserValidator()
        {
            RuleFor(user => user.Email).Must(this.ValidEmail);
            RuleFor(user => user.Password).NotEmpty();
            RuleFor(user => user.Password).Length(6, 20);
            RuleFor(user => user.Username).NotEmpty();
            RuleFor(user => user.Username).Length(6, 20);
        }

        /// <summary>
        /// Validate the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>true if email is valid in false if is not valid</returns>
        private bool ValidEmail(string email)
        {
            this.log.Info("validation email : " + email);
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
