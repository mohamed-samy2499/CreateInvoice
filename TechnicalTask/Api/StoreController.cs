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
    public class StoreController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public IMapper Mapper { get; }

        //using the unitOfWork and the automapper Service by DI
        public StoreController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            Mapper = mapper;
        }
        // GET: api/<StoreController>
        [HttpGet]
        public async Task<IEnumerable<Store>> Get()
        {
            //get the Stores in the table
            return  await unitOfWork.StoreRepository.GetAll();
        }
        // GET: api/<StoreController>/Details
        [HttpGet("Details")]
        public async Task<IEnumerable<Store>> GetDetails()
        {
            //get the Stores in the table
            return await unitOfWork.StoreRepository.GetAllWithInvoicesAsync();
        }
        // GET api/<StoreController>/5
        [HttpGet("{id}"),EnsureInvoiceExists]

        public async Task<Store> Get(int id)
        {
            return  await unitOfWork.StoreRepository.GetDetailsById(id);
        }

        // POST api/<StoreController>
        //create
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] Store store)
        {
            //check if the store sent is not null
            if (store == null)
                return BadRequest();
            //add the store to the invoices table
            await unitOfWork.StoreRepository.Add(store);
            return Ok();
        }

        // PUT api/<StoreController>/5
        [HttpPut("{id}")]
        public async Task<StatusCodeResult> Put(int id, [FromBody] Store store)
        {
            if (store.Id != id)
                return BadRequest();
            await unitOfWork.StoreRepository.Update(store);
            return Ok();
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public async Task<StatusCodeResult>  Delete(int id,Store store)
        {
            if (id != store.Id)
                return NotFound();
            try
            {
                var Store = unitOfWork.StoreRepository.GetById(id).Result;
                await unitOfWork.StoreRepository.Delete(store);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
