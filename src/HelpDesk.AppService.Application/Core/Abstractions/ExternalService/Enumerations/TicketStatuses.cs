namespace HelpDesk.AppService.Application.Core.Abstractions.ExternalService.Enumerations
{
    public enum TicketStatuses : byte
    {
        New = 1,
        Assigned = 2,
        InProgress = 3,
        OnHold = 4,
        Completed = 5,
        Cancelled = 6
    }
}
