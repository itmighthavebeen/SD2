
using System.ComponentModel.DataAnnotations;

namespace Dinner.Models
{
    public class DinnerOrder
    {
        public int Id { get; set; }

        /// <summary>
        /// example of Dinner record
        /// </summary>
        /// <example>Dinner Name </example>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <example>Restaurant Name</example>
        public string Restaurant { get; set; } = string.Empty;

        public List<MenuItem>? MenuItems { get; set; }
    }
}
