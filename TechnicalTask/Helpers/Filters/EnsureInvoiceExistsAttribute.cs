using BusinessLogicLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helper.Filters
{
    public class EnsureInvoiceExistsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var service = (InvoiceRepository)context.HttpContext
                .RequestServices.GetService(typeof(InvoiceRepository));
            var AccountingEntryId = (int)context.ActionArguments["id"];
            if (! service.EnsureEntityExists(AccountingEntryId).Result)
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
