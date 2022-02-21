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
        [HttpGet("address/{storeAddress}")]
        public IActionResult GetStoreInventory(string storeAddress)
        {
            try
            {   _planetPaintballStoresBL.ViewInventory(storeAddress);
                return Ok(_planetPaintballStoresBL.GetProductsByStoreAddress(storeAddress));
            }
            catch (SqlException)
            {
                return NotFound();
            }
        }

        // POST: api/StoreFront
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
