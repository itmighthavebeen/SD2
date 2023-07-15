
namespace Dinner.Models
{
    public class DinnerOrder
    {
        public int Id { get; set; }

        /// <summary>
        ///     example summary
        /// </summary>
        /// <example>type something</example>
        public string Name { get; set; } = string.Empty;
        public string Restaurant { get; set; } = string.Empty;

        public required List<MenuItem> MenuItems { get; set; }
    }
}
