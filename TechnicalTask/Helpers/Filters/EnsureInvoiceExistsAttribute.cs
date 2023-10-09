using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helper.Filters
{
    public class EnsureInvoiceExistsAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnsureInvoiceExistsAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var InvoiceId = (int)context.ActionArguments["id"];
            var invoice = _unitOfWork.InvoiceRepository.GetById(InvoiceId);
            if (invoice == null)
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
