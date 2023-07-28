using Dinner.Data;
using Dinner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dinner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]


    public class DinnerController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public DinnerController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        /// <summary>
        /// POST Creates a Dinner record which will include name of dinner type and can include the restaurant name and items ordered.
        /// </summary>
        /// <param name="newDinnerOrder">A New Dinner order record</param>
        /// <returns>All records and the newly created Dinner Order</returns>
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
        /// <response code="200">Added Successfully.  Returns the newly created item</response>
        /// <response code="400">Invalid Input Data - Make Sure id is integer</response>
        //Create a Dinner
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<List<DinnerOrder>>> AddDinnerOrder(DinnerOrder newDinnerOrder)
        {
            if (IsDigitOnly(newDinnerOrder.Id.ToString()))

            {
                //store in Db table
                appDbContext.DinnerOrders.Add(newDinnerOrder);

                await appDbContext.SaveChangesAsync();
                var allRecords = await appDbContext.DinnerOrders.ToListAsync();
                _ = await appDbContext.MenuItems.ToListAsync();
                return Ok(newDinnerOrder);
            }
            return BadRequest("Id must be an integer");
        }

        private static bool IsDigitOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// GET Returns all Dinner record and menu items.
        /// </summary>
        /// <returns>A list of all Dinner Orders with Menu Items in the database</returns>
        /// <remarks>
        /// Sample reponse:
        ///
        ///     GET /Dinner
        ///     {
        ///        "id": 121  
        ///        "name": "Vegan Americana near University", 
        ///        "restaurant": "Lucky Pig at Paradise Palms, 41 Lotihian, Edinburgh",
        ///         "menuItems": [
        ///         {
        ///             "id": 1,   
        ///          "name": "fried pickles"
        ///        
        ///          },
        ///          {
        ///             "id": 3324,
        ///          "name": "mac and cheese"
        ///           }
        ///      }
        ///      {
        ///        "id": 43,  
        ///        "name": "Vegan KFC",  
        ///        "restaurant": "any KFC Edinburgh Scotland",
        ///         "menuItems": [
        ///         {
        ///             "id": 32,   
        ///          "name": "Original Recipe vegan Chickn"
        ///        
        ///          },
        ///          {
        ///             "id": 23,  
        ///          "name": "vegan french fries"
        ///          }
        ///       }
        ///       {
        ///        "id": 436,  
        ///        "name": "Vegan PapaJohns",  
        ///        "restaurant": "any PapaJohns Scotland",
        ///         "menuItems": [
        ///         {
        ///             "id": 15,   
        ///          "name": "Vegan Cheese Bread"
        ///        
        ///         }
        ///        }
        ///       
        ///        
        ///
        /// </remarks>
        /// <response code="200">Returns all database records</response>
        /// <response code="400">If the item is null</response>
        /// <response code="500">Internal Server Error</response>
        //Get All DinnerOrders
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<DinnerOrder>>> GetAllDinnerOrders()
        {

            var dinnerOrders = await appDbContext.DinnerOrders.ToListAsync();
            _ = await appDbContext.MenuItems.ToListAsync();

            if (dinnerOrders != null)
            {
                return Ok(dinnerOrders);
            }
            else
            {
                return NotFound();
            }
        }


        /// <summary>
        /// GET By Id Returns a specific Dinner record by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Dinner Order with all Menu Items</returns>
        /// <remarks>
        /// Sample response where # is the entered Id:
        ///
        ///     GET /Dinner
        ///     {
        ///        "id": #,   // The id of the Dinner record requested
        ///        "name": "Cold Sandwiches",  
        ///        "restaurant": "Half Peach",
        ///         "menuItems": [
        ///         {
        ///             "id": 123,  
        ///          "name": "cuban"
        ///        
        ///          },
        ///          {
        ///             "id": 456,     
        ///          "name": "po boy sandwhich"
        /// </remarks>
        /// <response code="200">Return requested record by Id - SUCCESS </response>
        /// <response code="204">Record not Found - No Content </response>
        /// <response code="404">Not Found!</response>
        /// <response code="400">Client Error</response>
        /// <response code="500">Internal Server Error</response>

        //Get Dinner Order By Id
        [HttpGet("{id}", Name = nameof(GetOrderById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<ActionResult<List<DinnerOrder>>> GetOrderById(int id)

        {
            var dinnerOrders = await appDbContext.DinnerOrders.ToListAsync();
            var menuItems = await appDbContext.MenuItems.ToListAsync();

            if (dinnerOrders != null)
            {
                return Ok(dinnerOrders.FirstOrDefault(o => o.Id == id));
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// PUT a change to a Dinner Record.
        /// Change the Dinner Name, Restuarant Name and/or Change or Add a menu item
        /// </summary>
        /// <param name="id"></param>
        ///  /// <param name="dinnerOrder"></param>
        /// <returns>Changed Dinner Order record </returns>
        /// <remarks>
        /// Sample response where # is the entered Id:
        ///
        ///     PUT /Dinner
        ///     {
        ///        "id": #,   // The id of the Dinner record to be updated
        ///        "name": "Cold Sandwiches", -- updated name
        ///        "restaurant": "Half Peach" -- Updated Name,
        ///         "menuItems": [
        ///         {
        ///             "id": 0,   //system generated id for Menu Item if adding or id to update
        ///          "name": "cuban" -- Added item or updated name
        ///        
        ///          },
        ///          {
        ///             "id": 0,     ///system generated id for Menu Item if adding or id to update
        ///          "name": "po boy sandwhich" -- Added Item or updated name
        /// </remarks> 
        /// <response code="200">Return requested record by Id - SUCCESS </response>
        /// <response code="404">Not Found!</response>
        /// <response code="400">Client Error</response>
        /// <response code="500">Internal Server Error</response>
        //Make a Change (PUT) DinnerOrders

        [HttpPut("{id}", Name = nameof(dinnerOrder))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<DinnerOrder>> PutString(int id, [FromBody] DinnerOrder dinnerOrder)
        {
            var oldDinnerOrder = await appDbContext.DinnerOrders.FirstOrDefaultAsync(x => x.Id == id);

            if (oldDinnerOrder != null)
            {
                if (dinnerOrder.MenuItems != null)
                {
                    oldDinnerOrder.Name = dinnerOrder.Name;
                    oldDinnerOrder.Restaurant = dinnerOrder.Restaurant;
                    oldDinnerOrder.MenuItems = dinnerOrder.MenuItems;
                    await appDbContext.SaveChangesAsync();
                    return Ok(oldDinnerOrder);
                }
                else
                {
                    oldDinnerOrder.Name = dinnerOrder.Name;
                    oldDinnerOrder.Restaurant = dinnerOrder.Restaurant;
                    //  oldDinnerOrder.MenuItems = dinnerOrder.MenuItems;
                    await appDbContext.SaveChangesAsync();
                    return Ok(oldDinnerOrder);
                }

            }
            if (oldDinnerOrder == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }

        }


        /// <summary>
        /// DELETE a Dinner Record by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete Dinner Order record </returns>
        /// <remarks>
        /// Sample response where # is the entered Id:
        ///
        ///     GET /Dinner
        ///     {
        ///        "id": #,   // The id of the Dinner record to be deleted
        ///        "name": "Cold Sandwiches", 
        ///        "restaurant": "Half Peach"
        ///         "menuItems": [
        ///         {
        ///             "id": 23,  
        ///          "name": "cuban"
        ///        
        ///          },
        ///          {
        ///             "id": 54,    
        ///          "name": "po boy sandwhich"
        /// </remarks>
        /// <response code="200">Return requested record by Id - SUCCESS </response>
        /// <response code="404">Not Found!</response>
        /// <response code="400">Client Error</response>
        /// <response code="500">Internal Server Error</response>
        /// 
        // Delete A DinnerOrder by Id

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DinnerOrder>> DeleteOrder(int id)
        {

            //use Raw Sql to check if DB is empty

            var ordersFound = appDbContext.DinnerOrders
                 .FromSql($"SELECT * FROM DinnerOrders")
                 .ToList();
            if (ordersFound == null || ordersFound.Count == 0)
            {
                return NotFound();
            }

            var result = await appDbContext.DinnerOrders.Include(e => e.MenuItems).Where(o => o.Id == id).FirstAsync();

            if (result != null)
            {
                appDbContext.Remove(result);
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            if (result == null)
            {
                return NotFound();
            }
            return BadRequest();
        }

    }
}



