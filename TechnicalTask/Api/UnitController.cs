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
    public class UnitController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public IMapper Mapper { get; }

        //using the unitOfWork and the automapper Service by DI
        public UnitController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            Mapper = mapper;
        }
        // GET: api/<ItemController>
        [HttpGet]
        public async Task<IEnumerable<Unit>> Get()
        {
            //get the Items in the table
            return  await unitOfWork.UnitRepository.GetAll();
        }
        // GET api/<ItemController>/5
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            var unit = await unitOfWork.UnitRepository.GetById(id);
            if(unit != null)
                return  Ok(unit);
            return BadRequest();
        }

        // POST api/<ItemController>
        //create
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] Unit Unit)
        {
            //check if the Item sent is not null
            if (Unit == null)
                return BadRequest();
            //add the Item to the invoices table
            await unitOfWork.UnitRepository.Add(Unit);
            return Ok();
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public async Task<StatusCodeResult> Put(int id, [FromBody] Unit Unit)
        {
            if (Unit.Id != id)
                return BadRequest();
            await unitOfWork.UnitRepository.Update(Unit);
            return Ok();
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public async Task<StatusCodeResult>  Delete(int id,Unit unit)
        {
            if (id != unit.Id)
                return NotFound();
            try
            {
                var Unit = unitOfWork.UnitRepository.GetById(id).Result;
                await unitOfWork.UnitRepository.Delete(Unit);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
