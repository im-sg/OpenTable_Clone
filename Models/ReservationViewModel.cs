namespace OpenTable.Models
{
    public class ReservationViewModel
    {
        public string ActivePriceRange { get; set; } = "all";
        public string ActiveCuisines { get; set; } = "all";
        public string ActiveMetropolis { get; set; } = "all";

        public string CheckActiveMetropolis(string d) =>
            d.ToLower() == ActiveMetropolis.ToLower() ? "active" : "";
        public string CheckActivePriceRange(string d) =>
            d.ToLower() == ActivePriceRange.ToLower() ? "active" : "";
        public string CheckActiveCuisines(string d) =>
            d.ToLower() == ActiveCuisines.ToLower() ? "active" : "";
        public List<PriceRange> PricesRange { get; set; } = new List<PriceRange>();
        public List<Metropolis> Metropolis { get; set; } = new List<Metropolis>();
        public List<Reservation> Reservation { get; set; } = new List<Reservation>();
        public List<Cuisines> Cuisines { get; set; } = new List<Cuisines>();
        public int selectedCuisinesID { get; set; } = 0;
        public int selectedPriceRangeID { get; set; } = 0;
        public int selectedMetropolisID { get; set; } =0;
        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public PriceRange PriceRangs { get; set; } = new PriceRange();
        public Cuisines Cuisine { get; set; } = new Cuisines();
        public Metropolis Metro { get; set; } = new Metropolis();
        public Restaurant Restaurant { get; set; } = new Restaurant();

    }
}
