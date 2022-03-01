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
    public class StoreFrontController : ControllerBase
    {

        private IPlanetPaintballBL _planetPaintballBl;
        private IPlanetPaintballStoresBL _planetPaintballStoresBL;

        public StoreFrontController(IPlanetPaintballBL p_planetPaintballBL, IPlanetPaintballStoresBL p_planetPaintballStoresBL)
        {
            _planetPaintballBl = p_planetPaintballBL;
            _planetPaintballStoresBL = p_planetPaintballStoresBL;
        }

        // GET: api/StoreFront
        [HttpGet("GetAllStores")]
        public IActionResult GetAllStores()
        {
            try
            {
                return Ok(_planetPaintballStoresBL.ViewAllStores());
            }
            catch (SqlException)
            {
                return NotFound();
            }
        }

        // GET: api/StoreFront/5
        //gets the inventory of a store by the address
        [HttpGet("GetStoreInventory")]
        public IActionResult GetStoreInventory([FromQuery] string storeAddress)
        {
            try
            {   
                _planetPaintballStoresBL.ViewInventory(storeAddress);
                return Ok(_planetPaintballStoresBL.GetProductsByStoreAddress(storeAddress));
            }
            catch (SqlException)
            {
                return NotFound();
            }
        }

        [HttpGet("ViewOrderHistory")]
        public IActionResult ViewOrderHistory([FromQuery] string searchedString, string searchMode)
        {
            try
            {   
                return Ok(_planetPaintballStoresBL.GetOrders(searchedString, searchMode));
            }
            catch (SqlException e)
            {
                return NotFound(new {Results = e.Message});
            }
        }

        // POST: api/StoreFront
        [HttpPost("ReplenishInventory")]
        public IActionResult ReplenishInventory(string p_managerEmail, string p_managerPassword, int p_storeID, int p_productID, int p_quantity)
        {
            try
            {
                Log.Information("Verifying if user is a manager for a store.");
                bool isManager = _planetPaintballStoresBL.VerifyManager(p_managerEmail, p_managerPassword, p_storeID);
                if(isManager)
                {
                    Log.Information("User is a manager and updated store inventory!");
                    _planetPaintballStoresBL.UpdateInventory(p_storeID, p_productID, p_quantity);
                    return Ok();
                }
                else
                {
                    Log.Warning("Could not update store inventory. User might not be a manager or they are trying to add to the wrong store");
                    Exception exc = new Exception ("Manager Credentials did not match for that store.");
                    return Conflict(exc);
                }
            }
            catch(SqlException)
            {
                Log.Warning("Could not find store or product to add inventory to");
                return NotFound();
            }
        }

        [HttpPost("PlaceOrder")]
        public IActionResult PlaceAnOrder(Orders p_order)
        {
            try
            {
                Log.Information("User started placing the order!");
                _planetPaintballStoresBL.StartOrder(p_order);
                Log.Information("User placed their order!");
                return Created("Successfully placed order",_planetPaintballStoresBL.MakeAnOrder(p_order));
            }
            catch(System.Exception ex)
            {
                Log.Warning("The order was not able to be made");
                _planetPaintballStoresBL.DeleteOrder(p_order);
                return Conflict(ex.Message);
            }
        }

        // PUT: api/StoreFront/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/StoreFront/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
