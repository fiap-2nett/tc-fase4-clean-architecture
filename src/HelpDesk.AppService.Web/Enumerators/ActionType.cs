namespace HelpDesk.AppService.Web.Enumerators
{
    internal enum ActionType : byte
    {
        AssignTo = 1,
        AssignToMe = 2,
        ChangeStatusTo = 3,
        ChangeStatusToComplete = 4,
        Cancellation = 5
    }
}
