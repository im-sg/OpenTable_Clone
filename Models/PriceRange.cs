using System.ComponentModel.DataAnnotations;

namespace OpenTable.Models
{
    public class PriceRange
    {
        public int PriceRangeId { get; set; }
        public string PriceRanges { get; set; } = string.Empty;
    }
}
