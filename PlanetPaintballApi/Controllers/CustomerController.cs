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
                return Ok(_planetPaintballBL.GetCustomers());
            }
            catch (SqlException)
            {
                return NotFound();
            }
        }   

        //GET: api/Customer/5
        [HttpGet("{customerInfo}")]
        public IActionResult GetCustomer(string searchMode, string customerInfo)
        {
            try
            {
                return Ok(_planetPaintballBL.SearchCustomer(searchMode, customerInfo));
            }
            catch (SqlException)
            {
                return NotFound();
            }
        }

        [HttpGet("name/{customerName}")]
        public IActionResult GetCustomerByName(string customerName)
        {
            try
            {
                return Ok(_planetPaintballBL.SearchCustomerByName(customerName));
            }
            catch (SqlException)
            {
                return NotFound();
            }
        }

        [HttpGet("email/{customerEmail}")]
        public IActionResult GetCustomerByEmail(string customerEmail)
        {
            try
            {
                return Ok(_planetPaintballBL.SearchCustomerByEmail(customerEmail));
            }
            catch (SqlException)
            {
                return NotFound();
            }
        }

        // POST: api/Customer
        [HttpPost("AddCustomer")]
        public IActionResult Post([FromBody] Customer p_customer)
        {
            try
            {
                return Created("Successfully added", _planetPaintballBL.AddCustomer(p_customer));
            }
            catch (System.Exception ex)
            {
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
