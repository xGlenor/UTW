using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Models;
using ServerUTW.Data;
using BaseLibrary.Contracts;
using BaseLibrary.GenericModels;
using BaseLibrary.Responses;

namespace ServerUTW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesController : ControllerBase
    {
        private readonly IFeeRepository _repository;

        public FeesController(IFeeRepository context)
        {
            _repository = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fee>>> GetFees()
        {
            return await _repository.GetAll();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Fee>> GetFee(int id)
        {
            var fee = await _repository.GetById(id);

            if (fee == null)
            {
                return NotFound();
            }

            return fee;
        }

        
        [HttpPut("{id:int}")]
        public async Task<EntityResponse> PutStudent(int id, Fee fee)
        {
            if (id != fee.Id)
            {
                return new EntityResponse(false, "Bad ID", null);
            }

            var result = await _repository.Update(id, fee);

            return new EntityResponse(true, "Updated successfully", Generics.SerializeObj(result));
        }

        [HttpPost]
        public async Task<ActionResult<Fee>> PostFee(Fee fee)
        {
            var FeeAdded = await _repository.Insert(fee);

            return CreatedAtAction("GetFee", new { id = fee.Id }, fee);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFee(int id)
        {
            var fee = await _repository.GetById(id);
            if (fee == null)
            {
                return NotFound();
            }

            var feeDeleted = await _repository.Delete(fee.Id);

            return Ok(new { Message = "Pomyślnie usunięto opłate", Fee = feeDeleted });
        }

        
    }
}
