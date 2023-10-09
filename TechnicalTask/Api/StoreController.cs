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
        public async Task<IActionResult> Get()
        {
            //get the Stores in the table
            var stores = await unitOfWork.StoreRepository.GetAll();
            //map it to the view model
            var storesVm = Mapper.Map<IEnumerable<Store>, IEnumerable<StoreViewModel>>(stores);
            //var storesVm = stores.Select(i => new StoreViewModel
            //{
            //    Id = i.Id,
            //    Name = i.Name
            //});

            return Ok(storesVm);
        }


        // POST api/<StoreController>
        //create
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] StoreViewModel storeVm)
        {
            //check if the store sent is not null
            if (storeVm == null)
                return BadRequest();
            //add the store to the invoices table
            var store = Mapper.Map<StoreViewModel, Store>(storeVm);
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
