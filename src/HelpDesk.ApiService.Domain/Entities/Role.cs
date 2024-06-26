using System;
using HelpDesk.ApiService.Domain.Core.Abstractions;
using HelpDesk.ApiService.Domain.Core.Primitives;
using HelpDesk.ApiService.Domain.Core.Utility;

namespace HelpDesk.ApiService.Domain.Entities
{
    public sealed class Role : Entity<byte>, IAuditableEntity, ISoftDeletableEntity
    {
        #region Properties

        public string Name { get; private set; }
        public string Description { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdatedAt { get; private set; }

        #endregion

        #region Constructors

        private Role()
        { }

        public Role(byte idRole, string name, string description = null)
            : base(idRole)
        {
            Ensure.GreaterThan(idRole, 0, "The role identifier must be greater than zero.", nameof(idRole));
            Ensure.NotEmpty(name, "The role name is required.", nameof(name));

            Name = name;
            Description = description;
        }

        #endregion
    }
}
