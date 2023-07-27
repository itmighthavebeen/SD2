
using System.ComponentModel.DataAnnotations;

namespace Dinner.Models
{
    public class DinnerOrder
    {
        public int Id { get; set; }

        /// <summary>
        /// example of Dinner record
        /// </summary>
        /// <example>REQUIRED dinner name for example mexican, vegan, sandwiches, etc.</example>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <example>Name of restaurant where this meal is ordered</example>
        public string Restaurant { get; set; } = string.Empty;

        public required List<MenuItem> MenuItems { get; set; }
    }
}
