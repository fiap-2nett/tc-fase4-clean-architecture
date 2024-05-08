namespace HelpDesk.AppService.Web.Models.TicketViewModels
{
    public sealed class TicketActionViewModel
    {
        public int IdTicket { get; set; }
        public int IdActionType { get; set; }
        public int? IdUserAssigned { get; set; }
        public byte? IdStatusChanged { get; set; }
        public string CancellationReason { get; set; }
    }
}
