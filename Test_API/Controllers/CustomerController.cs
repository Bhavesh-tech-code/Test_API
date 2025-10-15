using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_API.Data;
using Test_API.Models;

namespace Test_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public CustomerController(ApplicationContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()  ///http://localhost:5057/Customer/GetAllCustomers
        {
            var data = _context.Customers.ToList();
            if (data.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet("GetCustomerById/{id}")]    ///http://localhost:5057/Customer/GetCustomerById/1
        public IActionResult GetCustomerById(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var data = _context.Customers.Where(e => e.Id == id).SingleOrDefault();
                if (data == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(data);
                }
            }
        }

        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer([FromBody] Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                //_context.Customers.Add(model); first way do the same
                var data = new Customer
                {
                    Name = model.Name,
                    Gender = model.Gender,
                    IsActive = model.IsActive
                };
                _context.Customers.Add(data);
                _context.SaveChanges();
                return Ok("Record inserted successfully!");
            }
        }

        [HttpPut("UpdateCustomer")]
        public IActionResult UpdateCustomer([FromBody] Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var data = _context.Customers.Where(e => e.Id == model.Id).SingleOrDefault();
                if (data == null)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    data.Name = model.Name;
                    data.Gender = model.Gender;
                    data.IsActive = model.IsActive;
                    _context.Customers.Update(data);
                    _context.SaveChanges();
                    return Ok("Record updated Successfully!");
                }
            }
        }

        [HttpDelete("DeleteCustomer/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (id != 0)
            {
                var data = _context.Customers.Where(e => e.Id == id).FirstOrDefault();
                if (data == null)
                {
                    return BadRequest();
                }
                else
                {
                    _context.Customers.Remove(data);
                    _context.SaveChanges();
                }
            }
            else
            {
                return BadRequest();
            }
            return Ok("Record deleted succesfully!");
        }
    }
}









