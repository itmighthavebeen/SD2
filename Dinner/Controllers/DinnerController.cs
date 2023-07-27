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


        /// <summary>
        /// Creates a Dinner record which will include name of dinner type and can include the restaurant name and items ordered.
        /// </summary>
        /// <param name="newDinnerOrder">A New Dinner order record</param>
        /// <returns>A newly created Dinner Order</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Dinner
        ///     {
        ///        "id": 0,   // Don't need to enter-system generated
        ///        "name": "Cold Sandwiches",  //Required!
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
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        //Create a Dinner
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
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


        /// <summary>
        /// GETS all Dinner records with their corresponding Menu Items
        /// </summary>
        /// <returns>A list of all Dinner Orders with Menu Items in the database</returns>
        /// <remarks>
        /// Sample reponse:
        ///
        ///     GET /Dinner
        ///     {
        ///        "id": 0,   // Don't need to enter-system generated
        ///        "name": "Cold Sandwiches",  //Required!
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
        ///      }
        ///       {
        ///        "id": 0,   // Don't need to enter-system generated
        ///        "name": "Vegan KFC",  //Required!
        ///        "restaurant": "any KFC Edinburgh Scotland",
        ///         "menuItems": [
        ///         {
        ///             "id": 0,   //leave as 0 - system generated
        ///          "name": "Original Recipe vegan Chicken"
        ///        
        ///          },
        ///          {
        ///             "id": 0,     ///leave as 0 - system generated
        ///          "name": "vegan french fries"
        ///        
        ///      }
        ///
        /// </remarks>
        /// <response code="200">Returns all database records</response>
        /// <response code="304">Redirection Not Modified</response>
        /// <response code="400">If the item is null</response>
        /// <response code="500">Internal Server Error</response>
      
        //Get All DinnerOrders
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<DinnerOrder>>> GetAllDinnerOrders()
        {
           
            
                var dinnerOrders = await appDbContext.DinnerOrders.ToListAsync();
                _ = await appDbContext.MenuItems.ToListAsync();
              
            //    return Ok(dinnerOrders);
                if (dinnerOrders != null)
                {
                    //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
                    return Ok(dinnerOrders);
                }
                else
                {
                    //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
                    return NotFound();
                }
        }


        /// <summary>
        /// Get a specific Dinner record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of all Dinner Orders with Menu Items in the database</returns>
        /// <remarks>
        /// Sample response where # is the entered Id:
        ///
        ///     GET /Dinner
        ///     {
        ///        "id": #,   // The id of the Dinner record requested
        ///        "name": "Cold Sandwiches",  //Required!
        ///        "restaurant": "Half Peach",
        ///         "menuItems": [
        ///         {
        ///             "id": 0,   //system generated id for Menu Item
        ///          "name": "cuban"
        ///        
        ///          },
        ///          {
        ///             "id": 0,     ///system generated id for Menu Item
        ///          "name": "po boy sandwhich"
        /// </remarks>
        /// <response code="200">Return requested record by Id - SUCCESS </response>
        /// <response code="404">Not Found!</response>
        /// <response code="400">Client Error</response>
        /// <response code="500">Internal Server Error</response>

        //Get Dinner Order By Id
        [HttpGet("{id}", Name =nameof(GetOrderById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<ActionResult<List<DinnerOrder>>> GetOrderById(int id)
        
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
        }

        /// <summary>
        /// PUT a change to a Dinner Record.
        /// Change the Dinner Name, Restuarant Name and/or ADD a menu item
        /// </summary>
        /// <param name="id"></param>
        ///  /// <param name="dinnerOrder"></param>
        /// <returns>Changed Dinner Order record </returns>
        /// <remarks>
        /// Sample response where # is the entered Id:
        ///
        ///     GET /Dinner
        ///     {
        ///        "id": #,   // The id of the Dinner record requested
        ///        "name": "Cold Sandwiches",  //Required!
        ///        "restaurant": "Half Peach",
        ///         "menuItems": [
        ///         {
        ///             "id": 0,   //system generated id for Menu Item
        ///          "name": "cuban"
        ///        
        ///          },
        ///          {
        ///             "id": 0,     ///system generated id for Menu Item
        ///          "name": "po boy sandwhich"
        /// </remarks>
        /// <response code="200">Return requested record by Id - SUCCESS </response>
        /// <response code="404">Not Found!</response>
        /// <response code="400">Client Error</response>
        /// <response code="500">Internal Server Error</response>
        //Make a Change (PUT) DinnerOrders

        [HttpPut ("{id}", Name =nameof(dinnerOrder))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<DinnerOrder>> PutString(int id, [FromBody] DinnerOrder dinnerOrder)
        {
            var oldDinnerOrder = await appDbContext.DinnerOrders.FirstOrDefaultAsync(x => x.Id == id);
            //    var oldMenuItem = await appDbContext.MenuItems.ToListAsync();
            //   var thisItem = new List<MenuItem>;

            //  var oldMenuItems = await appDbContext.MenuItems.FirstOrDefault(x => x.)

            //  foreach (var Name in oldMenuItem) { 
            //  oldMenuItem = dinnerOrder.MenuItems.FirstOrDefault(x =>x.Id == id)};
            if (oldDinnerOrder != null)
            {
                oldDinnerOrder.Name = dinnerOrder.Name;
                oldDinnerOrder.Restaurant = dinnerOrder.Restaurant;
                oldDinnerOrder.MenuItems = dinnerOrder.MenuItems;
            }

            else
            {

                oldDinnerOrder.Name = oldDinnerOrder.Name;
                oldDinnerOrder.Restaurant = oldDinnerOrder.Restaurant;
                oldDinnerOrder.MenuItems = oldDinnerOrder.MenuItems;
            }

             await appDbContext.SaveChangesAsync();
            return Ok(oldDinnerOrder);

        }

        [HttpDelete("{id}")]

        //  public ActionResult<int> DeleteString(int id)

      //  public async Task<ActionResult<List<DinnerOrder>>> DeleteOrder(int id)
     //   {
            /*this works
                     //   var dinnerOrders =  appDbContext.DinnerOrders.ToListAsync();
                        var blog = appDbContext.DinnerOrders.Where(e => e.Id == id).Include(e => e.MenuItems).First();
                         appDbContext.Remove(blog);

                         appDbContext.SaveChanges();
                         return Ok(blog);
            */

              public async Task<ActionResult<DinnerOrder>>DeleteOrder(int id)
              {
            var result = await appDbContext.DinnerOrders.Include(e => e.MenuItems).Where(o =>o.Id == id).FirstAsync();
        //    var result = await appDbContext.DinnerOrders.Include(e => e.MenuItems).Where(o => o.Id == id).ToListAsync();
            //    var result = await appDbContext.DinnerOrders
            //        .SingleOrDefaultAsync(e => e.Id == id);
            //     .Include(equals => equals.MenuItems).First();
            if (result != null)
                  {
               //     appDbContext.DinnerOrders.Remove(result);
               appDbContext.Remove(result);
                await appDbContext.SaveChangesAsync();
                      return Ok();
                  }

                  return null;
              }

         /*   var product = await appDbContext.DinnerOrders.SingleOrDefaultAsync(m => m.Id == id);
            var stuff = await appDbContext.MenuItems.FirstOrDefaultAsync(c => c.DinnerOrders.Id == id);
            appDbContext.DinnerOrders.Remove(product);
            appDbContext.MenuItems.Remove(stuff);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));*/
      //  }

        //   return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
        /*  var dinnerOrders = await appDbContext.DinnerOrders.ToListAsync();
          var menuItems = await appDbContext.MenuItems.ToListAsync();*/




        //   var dinnerOrder = await appDbContext.DinnerOrders.FirstOrDefaultAsync(x => x.Id == id); 
        //  if (dinnerOrder != null) 
        //  {
        //     appDbContext.DinnerOrders.Remove(dinnerOrder).;

        //  if (menuItems != null)
        //   {
        //       appDbContext.MenuItems.Remove(menuItems.FirstOrDefault(x => x.DinnerOrderId == id));
        //   }

        //    await appDbContext.SaveChangesAsync();
        //    return Ok(await appDbContext.DinnerOrders.ToListAsync());
        //  }

        //  return NotFound();




        //         
        //    var    dinnerOrders = await appDbContext.DinnerOrders.ToListAsync();
        //  var dinnerOrders =  appDbContext.DinnerOrders.Where(x => x.Id == id);
        //  appDbContext.Remove(dinnerOrders.Where(x => x.Id == id));
        /*    
              var  whatDeleted = id;
        //      var menuItems =(appDbContext.MenuItems.Where(x => x.DinnerOrderId == id))  ;
              //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
              if (dinnerOrders != null)
              {
                  //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
                  //   appDbContext.Remove(dinnerOrders.FirstOrDefault(o => o.Id == id).MenuItems);
                  //this removed the row var blog = appDbContext.DinnerOrders.Where(e => e.Id == id).Include(e => e.MenuItems).First();
                  var blog = appDbContext.DinnerOrders.Where(e => e.Id == id);
                var dinnerOrders = await appDbContext.DinnerOrders.ToListAsync();
                  var menuItems = await appDbContext.MenuItems.ToListAsync();

                  foreach (var DinnerOrderId in menuItems)
                  {
                      appDbContext.Remove(menuItems);
                  }

                  //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
                  if (dinnerOrders != null)
                  {
                      //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
                    //  (dinnerOrders.FirstOrDefault(o => o.Id == id));
                      return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
                  }
                      //       appDbContext.Remove(menuItems.FirstOrDefault(o => o.DinnerOrderId == id));
                      appDbContext.Remove(blog);

                  appDbContext.SaveChanges();
                  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));

                  //   return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
              }
              else
              {
                  //  return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
                  return NotFound();
              }*/

        //      appDbContext.MenuItems.Remove(appDbContext.MenuItems.FirstOrDefault(o => o. == id));
        //appDbContext.DinnerOrders.Remove(appDbContext.DinnerOrders.FirstOrDefault(dinnerOrder => dinnerOrder.Id == id));

        //would love to use sqlite cascade deete feature

        //   var byebye = await appDbContext.dinnerOrders.Where(DinnerOrder => DinnerOrder.Id == id);


        //
        //    appDbContext.Remove(byebye);

        //  appDbContext.SaveChanges();
        //  return Ok( id);


    }

}
