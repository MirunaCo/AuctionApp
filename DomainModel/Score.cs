// *********************************************************************************
// <copyright file="Score.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************

namespace DomainModel
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class Score.
    /// </summary>
    public partial class Score
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        public double Points { get; set; }

        /// <summary>
        /// Gets or sets the user identifier to.
        /// </summary>
        /// <value>The user identifier to.</value>
        public int UserIdTo { get; set; }

        /// <summary>
        /// Gets or sets the user identifier from.
        /// </summary>
        /// <value>The user identifier from.</value>
        public int UserIdFrom { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the user from.
        /// </summary>
        /// <value>The user from.</value>
        public virtual User UserFrom { get; set; }
    }
}
