using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dinner.Models
{
    public class MenuItem
    {
        ///<Summary>
        /// Get these Menu Items
        ///</Summary>
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        ForeignKey DinnerOrderId { get; set; }
    }
}
