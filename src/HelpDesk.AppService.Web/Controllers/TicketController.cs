using System;
using System.Threading.Tasks;
using HelpDesk.AppService.Application.Core.Abstractions.Services;
using HelpDesk.AppService.Application.Services;
using HelpDesk.AppService.Web.Extensions;
using HelpDesk.AppService.Web.Models.AccountViewModels;
using HelpDesk.AppService.Web.Models.TicketViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.AppService.Web.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        #region Read-Only Fields

        private readonly ITicketService _ticketService;

        #endregion

        #region Constructors

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
        }

        #endregion

        #region Controller Actions

        [HttpGet]
        public IActionResult Index()
            => View();

        [HttpGet]
        public async Task<IActionResult> Categories()
            => View(await _ticketService.GetCategoriesAsync());

        [HttpGet]
        public async Task<IActionResult> Status()
            => View(await _ticketService.GetTicketStatusAsync());

        [HttpGet]
        public IActionResult Create()
            => View(new TicketViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _ticketService.CreateAsync(model.IdCategory, model.Description);
                if (result.IsSuccess)
                    return RedirectToAction(nameof(HomeController.Index), "Home");

                ModelState.AddErrors(result.Errors);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var result = await _ticketService.GetByIdAsync(id);
            if (!result.IsSuccess)
                throw new Exception("An error occurred while processing the request.");
            
            var model = (TicketViewModel)result.Ticket;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _ticketService.UpdateAsync(model.IdTicket, model.IdCategory, model.Description);
                if (result.IsSuccess)
                    return RedirectToAction(nameof(HomeController.Index), "Home");

                ModelState.AddErrors(result.Errors);
            }

            return View(model);
        }

        #endregion        
    }
}
