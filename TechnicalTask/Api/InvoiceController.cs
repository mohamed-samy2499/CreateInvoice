using API.Helper;
using API.Helper.Filters;
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TechnicalTask.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechnicalTask.Api
{
    [Route("api/[controller]")]
    [ApiController, ValidateModel]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public IMapper Mapper { get; }

        //using the unitOfWork and the automapper Service by DI
        public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            Mapper = mapper;
        }
        // GET: api/<InvoiceController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {

                //get the invoices in the table
                var invoices =  await unitOfWork.InvoiceRepository.GetAllWithIncludes();
                //map the invoices to invoiceViewModel
                var invoicesVm = Mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceViewModel>>(invoices);
                return Ok(invoicesVm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<InvoiceController>/5
        [HttpGet("{id}")]

        public async Task<Invoice> Get(int id)
        {
            return  await unitOfWork.InvoiceRepository.GetById(id);
        }
        //[HttpGet("GetDetails/{id}")]

        //public async Task<IActionResult> GetDetails(int id)
        //{
        //    try
        //    {

        //        var details = await unitOfWork.InvoiceRepository.GetDetailsById(id);
        //        return Ok(details);
        //    }
        //    catch(Exception ex)
        //    {
        //        var msg = ex.Message;
        //        return BadRequest(msg);
        //    }
        //}
        // POST api/<InvoiceController>
        //create
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] InvoiceViewModel invoiceVm)
        {
            try
            {

                //check if the incoice view  model sent is not null
                if (invoiceVm == null)
                    return BadRequest();
                //check if the InvoiceItems are not null
                if (invoiceVm.InvoiceItemsViewModel == null)
                    return BadRequest();
                if (!CheckCalculations(invoiceVm))
                {
                    ModelState.AddModelError("", "Total and Net values do not match the calculations.");
                    return BadRequest();
                }
                //map the view model to our invoice model
                var invoice = Mapper.Map<Invoice>(invoiceVm);
                //add the invoice to the invoices table
                invoice.Store = await unitOfWork.StoreRepository.GetById(invoiceVm.StoreId);
                await unitOfWork.InvoiceRepository.Add(invoice);
                //loop through the invoiceitems associated with the invoice and add them to the invoiceitem table

                foreach(var invoiceItemVm in invoiceVm.InvoiceItemsViewModel)
                {
                    invoiceItemVm.InvoiceViewModelId = invoice.Id;
                    invoiceItemVm.Item = await unitOfWork.ItemRepository.GetById(invoiceItemVm.ItemId);

                    var invoiceItem = Mapper.Map<InvoiceItemViewModel, InvoiceItem>(invoiceItemVm);
                    var res = await unitOfWork.InvoiceItemRepository.Add(invoiceItem);
                }
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpGet("AddInvoiceItemRow/{index}")]
        public  async Task<InvoiceItemViewModel?> AddInvoiceItemRow(int index)
            {
            var viewModel = new InvoiceItemViewModel
            {
                Sequence = index,
                AvailableItems =   unitOfWork.ItemRepository.GetAll().Result.Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                }),
                AvailableUnits = unitOfWork.UnitRepository.GetAll().Result.Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                })
            };

            return  viewModel;
        }
        // PUT api/<InvoiceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InvoiceController>/5
        #region Delete
        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                // Check if the invoice with the given ID exists
                var invoice = await unitOfWork.InvoiceRepository.GetById(id);

                if (invoice == null)
                {
                    return NotFound(); // Return a 404 Not Found response if the invoice doesn't exist
                }

                // Delete the invoice
                await unitOfWork.InvoiceRepository.Delete(invoice);

                return NoContent(); // Return a 204 No Content response on successful deletion
            }
            catch (Exception ex)
            {
                // Handle other exceptions and return an error response
                return StatusCode(500, "An error occurred while deleting the invoice.");
            }
        }
        #endregion
        private bool CheckCalculations(InvoiceViewModel invoiceVm)
        {
            decimal serverSideNet = 0;

            foreach (var item in invoiceVm.InvoiceItemsViewModel)
            {
                // Perform calculations similar to client-side
                decimal itemTotal = item.Quantity * item.Price;
                decimal itemNet = itemTotal - (itemTotal * (item.Discount / 100));

                serverSideNet += itemNet;
            }
            serverSideNet = (serverSideNet + (serverSideNet * (invoiceVm.Taxes / 100)));
            if ( serverSideNet != invoiceVm.Net)
                return false;
            return true;
        }
    }
}
