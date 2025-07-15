using System.ComponentModel.DataAnnotations;

namespace OpenTable.Models
{
    public class Cuisines
    {
        public int CuisinesId { get; set; }
        public string Cuisine { get; set; } = string.Empty;
    }
}
