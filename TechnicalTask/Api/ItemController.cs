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
    public class ItemController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public IMapper Mapper { get; }

        //using the unitOfWork and the automapper Service by DI
        public ItemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            Mapper = mapper;
        }
        // GET: api/<ItemController>
        [HttpGet]
        public async Task<IEnumerable<Item>> Get()
        {
            //get the Items in the table
            return  await unitOfWork.ItemRepository.GetAll();
        }
        // GET api/<ItemController>/5
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            var invoice = await unitOfWork.ItemRepository.GetById(id);
            if (invoice != null)
                return  Ok(invoice);
            return BadRequest();
        }

        // POST api/<ItemController>
        //create
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] Item Item)
        {
            //check if the Item sent is not null
            if (Item == null)
                return BadRequest();
            //add the Item to the invoices table
            await unitOfWork.ItemRepository.Add(Item);
            return Ok();
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public async Task<StatusCodeResult> Put(int id, [FromBody] Item Item)
        {
            if (Item.Id != id)
                return BadRequest();
            await unitOfWork.ItemRepository.Update(Item);
            return Ok();
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public async Task<StatusCodeResult>  Delete(int id,Item item)
        {
            if (id != item.Id)
                return NotFound();
            try
            {
                var Item = unitOfWork.ItemRepository.GetById(id).Result;
                await unitOfWork.ItemRepository.Delete(Item);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
