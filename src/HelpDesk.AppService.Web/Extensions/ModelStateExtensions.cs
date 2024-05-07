using HelpDesk.AppService.Application.Core.Abstractions.ExternalService.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HelpDesk.AppService.Web.Extensions
{
    public static class ModelStateExtensions
    {
        #region Extension Methods

        public static void AddErrors(this ModelStateDictionary modelState, params ErrorModel[] errors)
            => errors.ForEach(error => modelState.AddModelError(string.Empty, error.Message));

        #endregion
    }
}
