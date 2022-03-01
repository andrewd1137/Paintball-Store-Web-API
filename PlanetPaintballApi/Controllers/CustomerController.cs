using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PPBL;
using PPModel;



namespace PlanetPaintballApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private IPlanetPaintballBL _planetPaintballBL;

        public CustomerController(IPlanetPaintballBL p_planetPaintballBL)
        {
            _planetPaintballBL = p_planetPaintballBL;
        }

        // GET: api/Customer
        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                Log.Information("User has gotten all customers");
                return Ok(_planetPaintballBL.GetCustomers());
            }
            catch (SqlException)
            {
                Log.Warning("Could not return customers!");
                return NotFound();
            }
        }   

        //GET: api/Customer/5
        [HttpGet("SearchCustomer")]
        public IActionResult GetCustomer([FromQuery] string customerInfo)
        {
            try
            {
                Log.Information("User has searched for a customer");
                return Ok(_planetPaintballBL.SearchCustomer(customerInfo));
            }
            catch (SqlException)
            {
                Log.Warning("Could not find customer user searched for!");
                return NotFound();
            }
        }

        [HttpGet("VerifyCustomerCredentials")]
        public IActionResult GetCustomerCredentials([FromQuery] string customerEmail, string customerPassword)
        {
            try
            {
                Log.Information("User has verified customer credentials");
                _planetPaintballBL.VerifyCustomer(customerEmail, customerPassword);
                return Ok();
            }
            catch (SqlException)
            {
                Log.Warning("User with these credentials do not match!");
                return NotFound();
            }
        }

        // POST: api/Customer
        [HttpPost("AddCustomer")]
        public IActionResult Post([FromBody] Customer p_customer)
        {
            try
            {
                Log.Information("User has added a customer!");
                return Created("Successfully added", _planetPaintballBL.AddCustomer(p_customer));
            }
            catch (System.Exception ex)
            {
                Log.Warning("Could not add the user trying to be created!");
                return Conflict(ex.Message);
            }
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
