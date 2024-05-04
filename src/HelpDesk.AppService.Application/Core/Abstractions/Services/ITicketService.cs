using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.AppService.Application.Core.Abstractions.ExternalService.Models;

namespace HelpDesk.AppService.Application.Core.Abstractions.Services
{
    public interface ITicketService
    {
        #region ITicketService Members

        Task<IEnumerable<CategoryModel>> GetCategoriesAsync();
        Task<IEnumerable<StatusModel>> GetTicketStatusAsync();

        Task<PagedListResponseModel<TicketModel>> GetTicketsAsync(int page = 1, int pageSize = 10);
        Task<(bool IsSuccess, DetailedTicketModel Ticket, ErrorModel[] Errors)> GetByIdAsync(int idTicket);

        Task<(bool IsSuccess, int IdTicket, ErrorModel[] Errors)> CreateAsync(int idCategory, string description);
        Task<(bool IsSuccess, ErrorModel[] Errors)> UpdateAsync(int idTicket, int idCategory, string description);

        #endregion
    }
}
