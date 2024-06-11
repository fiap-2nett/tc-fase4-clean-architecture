namespace HelpDesk.ApiService.Domain.Core.Abstractions
{
    public interface ISoftDeletableEntity
    {
        #region ISoftDeletableEntity Properties

        bool IsDeleted { get; }

        #endregion
    }
}
