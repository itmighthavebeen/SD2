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
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Dinner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]

    public class DinnerController : ControllerBase
    {
        ///<Summary>Get these comments1</Summary>
        private readonly AppDbContext appDbContext;
        public DinnerController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

    /*   private readonly AppDbContext _context;

        public DinnerController(AppDbContext context) =>
            _context = context;
    */


        /// <summary>
        /// Creates a Dinner record which  can include name of dinner tpye, restaurant name and items ordered.
        /// </summary>
        /// <param name="newDinnerOrder">A New Dinner order record</param>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Dinner
        ///     {
        ///        "id": 0,   // Don't need to enter-system generated
        ///        "name": "Cold Sandwiches",
        ///        "restaurant": "Half Peach",
        ///         "menuItems": [
        ///         {
        ///             "id": 0,   //leave as 0 - system generated
        ///          "name": "cuban"
        ///        
        ///          },
        ///          {
        ///             "id": 0,     ///leave as 0 - system generated
        ///          "name": "po boy sandwhich"
        ///        
        ///          }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        //Create a Dinner
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<List<DinnerOrder>>> AddDinnerOrder(DinnerOrder newDinnerOrder)
        {
            if (newDinnerOrder != null)
            {
                //store in Db table
                appDbContext.DinnerOrders.Add(newDinnerOrder);
                
                await appDbContext.SaveChangesAsync();
                var allRecords = await appDbContext.DinnerOrders.ToListAsync();
                _ = await appDbContext.MenuItems.ToListAsync();
                return Ok(allRecords);
            }
            return BadRequest("Object instance not set");
        }
        //Get All DinnerOrders
        [HttpGet]
     //   [Produces("text/plain")]

        public async Task<ActionResult<List<DinnerOrder>>> GetAllDinnerOrders()
        {
            var dinnerOrders = await appDbContext.DinnerOrders.ToListAsync();
            _ = await appDbContext.MenuItems.ToListAsync();
            return Ok(dinnerOrders);
        }
        /// <summary>
        /// Get a specific Dinner record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Get Dinner Order By Id
        [HttpGet("{id}", Name =nameof(GetstringById))]

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

            if (oldDinnerOrder != null)
            {
                oldDinnerOrder.Name = dinnerOrder.Name;
                oldDinnerOrder.Restaurant = dinnerOrder.Restaurant;
                oldDinnerOrder.MenuItems = dinnerOrder.MenuItems;
            }
            appDbContext.SaveChanges();
            return Ok(oldDinnerOrder);

        }

        [HttpDelete("{id}")]

        //  public ActionResult<int> DeleteString(int id)


        //    {

        public async Task<ActionResult<List<DinnerOrder>>> RemoveById(int id)

        {
            var dinnerOrders = await appDbContext.DinnerOrders.ToListAsync();
            var menuItems = await appDbContext.MenuItems.ToListAsync();
            //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
            if (dinnerOrders != null)
            {
                //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
                appDbContext.Remove(dinnerOrders.FirstOrDefault(o => o.Id == id));  
                appDbContext.SaveChanges();
                return dinnerOrders;

                //   return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
            }
            else
            {
                //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
                return NotFound();
            }
            //           appDbContext.DinnerOrders.Remove(appDbContext.DinnerOrders.FirstOrDefault(dinnerOrder => dinnerOrder.Id == id));
            //        appDbContext.MenuItems.Remove(appDbContext.MenuItems.FirstOrDefault(dinnerOrder => dinnerOrder.Id == id));
         

            //would love to use sqlite cascade deete feature

            //  var byebye = appDbContext.DinnerOrders.Where(DinnerOrder => DinnerOrder.Id == id)



            //   appDbContext.Remove(byebye);

            ///  appDbContext.SaveChanges();
         //   return id;
        }

      }

}
