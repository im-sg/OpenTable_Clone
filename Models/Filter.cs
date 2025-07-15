namespace OpenTable.Models
{
    public class Filter
    {
        public Filter(string filtered)
        {
            filter = filtered ?? "all-all-all";
            string[] filteredSplit = filter.Split('-');
            CuisinesID = filteredSplit[0];
            PriceRangeID = filteredSplit[1];
            MetropolisID = filteredSplit[2];
        }
        public string filter { get; }
        public string CuisinesID { get; }
        public string PriceRangeID { get; }
        public string MetropolisID { get; }

        public bool HasCuisines => CuisinesID.ToLower() != "all";
        public bool HasPriceRanges => PriceRangeID.ToString().ToLower() != "all";
        public bool HasMetropolis => MetropolisID.ToString().ToLower() != "all";
    }
}
