namespace OpenTable.Models
{
    public class Sessions
    {
        private const string MetropolisKey = "SessionMetropolisKey";
        private const string PriceRangeKey = "SessionPriceRangeKey";
        private const string CuisinesKey = "SessionCuisinesKey";

        private ISession session { get; set; }
        public Sessions(ISession session) => this.session = session;
        public void SetActiveMetropolis(string activeMetropolis) =>
            session.SetString(MetropolisKey, activeMetropolis);
        public string GetActiveMetropolis() => 
            session.GetString(MetropolisKey) ?? string.Empty;
        public void SetActivePriceRange(string activePriceRange) =>
            session.SetString(PriceRangeKey, activePriceRange);
        public string GetActivePriceRange() => 
            session.GetString(PriceRangeKey) ?? string.Empty;
        public void SetActiveCuisines(string activeCuisines) =>
            session.SetString(CuisinesKey, activeCuisines);
        public string GetActiveCuisines() => 
            session.GetString(CuisinesKey) ?? string.Empty;
    }
}