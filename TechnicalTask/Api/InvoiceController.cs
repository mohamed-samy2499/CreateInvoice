using API.Helper;
using API.Helper.Filters;
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using TechnicalTask.Models;
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
        public async Task<IEnumerable<Invoice>> Get()
        {
            return await unitOfWork.InvoiceRepository.GetAllWithIncludes();
        }

        // GET api/<InvoiceController>/5
        [HttpGet("{id}"),EnsureInvoiceExists]

        public async Task<Invoice> Get(int id)
        {
            return  await unitOfWork.InvoiceRepository.GetDetailsById(id);
        }

        // POST api/<InvoiceController>
        //create
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] InvoiceViewModel invoiceVm)
        {
            if (invoiceVm == null)
            {
                return BadRequest();
            }
            var invoice = Mapper.Map<Invoice>(invoiceVm);
            await unitOfWork.InvoiceRepository.Add(invoice); 
            foreach(var invoiceItem in invoiceVm.InvoiceItems)
            {
                invoiceItem.Id = invoice.Id;
                await unitOfWork.InvoiceItemRepository.Add(invoiceItem);
            }
            return Ok();
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
