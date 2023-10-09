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
            return  await unitOfWork.InvoiceRepository.GetDetailsById(id);
        }

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
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
