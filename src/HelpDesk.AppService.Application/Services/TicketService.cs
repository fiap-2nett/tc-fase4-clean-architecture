using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using HelpDesk.AppService.Application.Core.Abstractions.Services;
using ExternalService = HelpDesk.AppService.Application.Core.Abstractions.ExternalService;
using HelpDesk.AppService.Application.Core.Abstractions.ExternalService.Models;


namespace HelpDesk.AppService.Application.Services
{
    public sealed class TicketService : ITicketService
    {
        #region Read-Only Fields

        private readonly ILogger _logger;

        private readonly ExternalService.ITicketService _ticketService;
        private readonly ExternalService.IStatusService _statusService;
        private readonly ExternalService.ICategoryService _categoryService;

        #endregion

        #region Constructors

        public TicketService(
            ILogger<AccountService> logger,
            ExternalService.ITicketService ticketService,
            ExternalService.IStatusService statusService,
            ExternalService.ICategoryService categoryService
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
            _statusService = statusService ?? throw new ArgumentNullException(nameof(statusService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        #endregion

        #region ITicketService Members

        public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
            => await _categoryService.GetAsync();

        public async Task<IEnumerable<StatusModel>> GetTicketStatusAsync()
            => await _statusService.GetAsync();

        public async Task<PagedListResponseModel<TicketModel>> GetTicketsAsync(int page = 1, int pageSize = 10)
            => await _ticketService.GetAsync(page, pageSize);

        public async Task<(bool IsSuccess, int IdTicket, ErrorModel[] Errors)> CreateAsync(int idCategory, string description)
            => await _ticketService.CreateAsync(idCategory, description);

        public async Task<(bool IsSuccess, DetailedTicketModel Ticket, ErrorModel[] Errors)> GetByIdAsync(int idTicket)
            => await _ticketService.GetByIdAsync(idTicket);

        public async Task<(bool IsSuccess, ErrorModel[] Errors)> UpdateAsync(int idTicket, int idCategory, string description)
            => await _ticketService.UpdateAsync(idTicket, idCategory, description);

        #endregion
    }
}
