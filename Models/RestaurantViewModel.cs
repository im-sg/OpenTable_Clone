
namespace OpenTable.Models
{
    public class RestaurantViewModel
    {
        public List<Restaurant> Restaurant { get; set; } = new List<Restaurant>();
        public List<Cuisines> Cuisines { get; set; } = new List<Cuisines>();
        public List<PriceRange> PriceRange { get; set; } = new List<PriceRange>();
        public List<Metropolis> Metropolis { get; set; } = new List<Metropolis>();
    }
}
