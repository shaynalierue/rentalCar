namespace RentalCars.Models
{
    public class CarWithImage
    {
        public string CarId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int? Year { get; set; } //Nullablle
        public decimal? TotalPrice { get; set; }
        public string Transmission { get; set; }       // Menyimpan data transmisi
        public int PassengerCapacity { get; set; }

        public DateTime? RentalDate { get; set; }  // Tanggal sewa
        public DateTime? ReturnDate { get; set; }  // Tanggal pengembalian
        public bool PaymentStatus { get; set; }
    }
}
