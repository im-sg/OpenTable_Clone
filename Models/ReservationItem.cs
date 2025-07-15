namespace OpenTable.Models
{
    public class ReservationItem
    {
        public int RestaurantId { get; set; }
        public DateTime Date { get; set; }
        public int Time { get; set; }
        public int People { get; set; }
    }
}
