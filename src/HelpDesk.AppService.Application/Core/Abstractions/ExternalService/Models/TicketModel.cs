namespace HelpDesk.AppService.Application.Core.Abstractions.ExternalService.Models
{
    public sealed record TicketModel(int IdTicket, string Description,
        CategoryModel Category, StatusModel Status, int IdUserRequester, int? IdUserAssigned);
}
