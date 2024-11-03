namespace RentalCars.Models
{
    public class CarRentalHistory
    {
        public DateTime RentalDate { get; set; }
        public string CarName { get; set; }
        public decimal PricePerDay { get; set; }
        public int TotalDays { get; set; }
        public decimal? TotalPrice { get; set; }
        public string PaymentStatus { get; set; }
    }
}
