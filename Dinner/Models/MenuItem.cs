using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dinner.Models
{
    public class MenuItem
    {
        ///<Summary>
        /// The  Menu Items that are ordered from the reaturant for the dinner
        ///</Summary>
        public int Id { get; set; }

        /// <example>Name(s) of the menu items that are ordered from the restaurant to make up the Dinner.For multiple Menu Items, copy code between and including brackets and separate with a comma.</example>

        public string Name { get; set; } = string.Empty;

    }
}
