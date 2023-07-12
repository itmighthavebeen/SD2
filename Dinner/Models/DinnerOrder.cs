
namespace Dinner.Models
{
    public class DinnerOrder
    {
        public int Id { get; set; }

        /// <summary>
        /// Name of the Order. This is a string value.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        public string Restaurant { get; set; } = string.Empty;

        public required List<MenuItem> MenuItems { get; set; }
    }
}
