using Dinner.Data;
using Dinner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using NuGet.Protocol;
using Microsoft.Identity.Client;
using System.Net;


namespace Dinner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    ///<Summary>
    /// Get these comments1
    ///</Summary>
    public class DinnerController : ControllerBase
    {
        ///<Summary>
        /// Get these comments1
        ///</Summary>
        private readonly AppDbContext appDbContext;
        public DinnerController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        /// <summary>
        /// Returns a collection of URLs
        /// </summary>
        /// <param name="slugs">list of slugs to retrieve</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/slugscollection/(d25tRx, fN5jpz)
        ///
        /// </remarks>
        /// <returns>IEnumerable of slugs</returns>
        /// <response code="200">If all requested items are 
        /// found</response>
        /// <response code="400">If slugs parameter is missing</response>
        /// <response code="404">If number of records found doesn't equal 
        /// number of records requested</response>

        //Create
        [HttpPost]

        ///<Summary>
        /// Get these comments1
        ///</Summary>

        public async Task<ActionResult<List<DinnerOrder>>> AddDinnerOrder(DinnerOrder newDinnerOrder)
        {
            if (newDinnerOrder != null)
            {
                //store in Db table
                appDbContext.DinnerOrders.Add(newDinnerOrder);
                
                await appDbContext.SaveChangesAsync();
                return Ok(await appDbContext.DinnerOrders.ToListAsync());
            }
            return BadRequest("Object instance not set");
        }
        //Get All DinnerOrders
        [HttpGet]

        public async Task<ActionResult<List<DinnerOrder>>> GetAllDinnerOrders()
        {
            var dinnerOrders = await appDbContext.DinnerOrders.ToListAsync();
            _ = await appDbContext.MenuItems.ToListAsync();
            return Ok(dinnerOrders);
        }

          //Get Dinner Order By Id
          [HttpGet("{id}")]

          public async Task<ActionResult<List<DinnerOrder>>> GetstringById(int id)
        
            {
            var dinnerOrders = await appDbContext.DinnerOrders.ToListAsync();
            var menuItems = await appDbContext.MenuItems.ToListAsync();
            //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
            if (dinnerOrders != null)
            {
                //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
                return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
            }
            else
            {
                //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
                return NotFound();
            }


            //    private ActionResult<List<DinnerOrder>> Ok(List<DinnerOrder> dinnerOrders, List<MenuItem> menuItems)
            //    {
            //        throw new NotImplementedException();
        }


        //Make a Change (PUT) DinnerOrders
        [HttpPut]

        public ActionResult<DinnerOrder> PutString(int id, DinnerOrder dinnerOrder)
        {
            var oldDinnerOrder = appDbContext.DinnerOrders.FirstOrDefault(x => x.Id == id);
            //  var oldMenuItems = appDbContext.MenuItems.FirstOrDefault(x => x.)
            oldDinnerOrder.Name = dinnerOrder.Name;
            oldDinnerOrder.Restaurant = dinnerOrder.Restaurant;
            appDbContext.SaveChanges();
            return Ok(oldDinnerOrder);

        }

      [HttpDelete("{id}")]

      public ActionResult<int> DeleteString(int id)
        {
            appDbContext.DinnerOrders.Remove(appDbContext.DinnerOrders.FirstOrDefault(dinnerOrder => dinnerOrder.Id == id));
            appDbContext.SaveChanges();
            return id;
        }

      }

}
