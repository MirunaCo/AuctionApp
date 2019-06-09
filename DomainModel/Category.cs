// *********************************************************************************
// <copyright file="Category.cs" company="Transilvania University of Brasov">
//     Miruna Coseriu
// </copyright>
// <summary>Miruna Coseriu</summary>
// *********************************************************************************

namespace DomainModel
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class Category.
    /// </summary>
    public partial class Category
    {
        /// <summary>Initializes a new instance of the <see cref="T:DomainModel.Category"/> class.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Risky to change manually")]
        public Category()
        {
            this.Categories = new HashSet<Category>();
            this.Product = new HashSet<Product>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>The category identifier.</value>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the parent category.
        /// </summary>
        /// <value>The parent category.</value>
        public virtual Category ParentCategory { get; set; }

        /// <summary>Gets or sets the categories.</summary>
        /// <value>The categories.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Risky to change manually")]
        public virtual ICollection<Category> Categories { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Risky to change manually")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
