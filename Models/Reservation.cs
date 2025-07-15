namespace OpenTable.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RestaurantId {  get; set; }
        public Restaurant Restaurant { get; set; } = new Restaurant();
        public DateTime ReservationDate { get; set; }
        public TimeSpan TimeSlot { get; set; }
        public int NumberOfGuests { get; set; }
        public string? Status { get; set; }
        public DateTime ReservationMadeAt { get; set; }
    }
}
