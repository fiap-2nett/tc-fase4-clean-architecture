using System;
using HelpDesk.ApiService.Domain.Core.Utility;
using HelpDesk.ApiService.Domain.Core.Primitives;
using HelpDesk.ApiService.Domain.Core.Abstractions;

namespace HelpDesk.ApiService.Domain.Entities
{
    public sealed class Category : Entity<int>, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties

        public byte IdPriority { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdatedAt { get; private set; }

        #endregion

        #region Constructors

        private Category()
        { }

        public Category(int idCategory, byte idPriority, string name, string description)
            : base(idCategory)
        {
            Ensure.GreaterThan(idCategory, 0, "The category identifier must be greater than zero.", nameof(idCategory));
            Ensure.GreaterThan(idPriority, 0, "The priority identifier must be greater than zero.", nameof(idCategory));
            Ensure.NotEmpty(name, "The category name is required.", nameof(name));

            IdPriority = idPriority;
            Name = name;
            Description = description;
        }

        #endregion
    }
}
