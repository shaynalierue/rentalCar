using Microsoft.EntityFrameworkCore;
using RentalCars.Data;
using RentalCars.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCars.Handlers
{
    public class RentCarHandler
    {
        private readonly AppDbContext _context;

        public RentCarHandler(AppDbContext context)
        {
            _context = context;
        }

        // Metode untuk mengambil data mobil (beserta gambar) berdasarkan carId
        public async Task<CarWithImage> GetCarWithImageByIdAsync(string carId, DateTime pickup, DateTime returns)
        {
            int rentalDays = (int)(returns.Date - pickup.Date).TotalDays + 1;// Hitung jumlah hari penyewaan

            var carWithImage = await _context.MsCar
                .Where(car => car.Car_id == carId)
                .Select(car => new CarWithImage
                {
                    CarId = car.Car_id,
                    Name = car.name,
                    Price = car.price_per_day,
                    Year = car.year,
                    Transmission = car.transmission,             // Ambil data Transmission dari MsCar
                    PassengerCapacity = car.number_of_car_seats, // Ambil data Jumlah Penumpang dari MsCar
                    Image = _context.MsCarImages
                        .Where(img => img.Car_id == car.Car_id)
                        .Select(img => img.image_link)
                        .FirstOrDefault() ?? "default.jpg",
                    TotalPrice = car.price_per_day * rentalDays  // Hitung total harga berdasarkan durasi sewa
                })
                .FirstOrDefaultAsync();

            return carWithImage;
        }

        public async Task<string> CreateRentalAsync(string carId, string customerName, DateTime pickup, DateTime returns, decimal totalPrice)
        {
            try
            {
                // Cari customer berdasarkan nama
                var customer = _context.MsCustomer.FirstOrDefault(c => c.email == customerName);
                if (customer == null)
                {
                    return "Customer tidak ditemukan.";
                }

                // Buat Rental_id dengan pola "RTDXXX"
                var lastRentalId = _context.TrRental
                    .OrderByDescending(r => r.Rental_id)
                    .FirstOrDefault()?.Rental_id;
                int newRentalIdNumber = lastRentalId != null && lastRentalId.StartsWith("RTD")
                    ? int.Parse(lastRentalId.Substring(3)) + 1
                    : 1;
                string newRentalId = $"RTD{newRentalIdNumber:D3}";

                // Buat Payment_id dengan pola "PYXXX"
                var lastPaymentId = _context.TrPayment
                    .OrderByDescending(p => p.Payment_id)
                    .FirstOrDefault()?.Payment_id;
                int newPaymentIdNumber = lastPaymentId != null && lastPaymentId.StartsWith("PY")
                    ? int.Parse(lastPaymentId.Substring(2)) + 1
                    : 1;
                string newPaymentId = $"PY{newPaymentIdNumber:D3}";

                // Buat entri baru untuk penyewaan di TrRental
                var rental = new TrRental
                {
                    Rental_id = newRentalId,
                    car_id = carId,
                    customer_id = customer.Customer_id,
                    rental_date = pickup,
                    return_date = returns,
                    total_price = totalPrice,
                    payment_status = false // Default: Belum dibayar
                };

                _context.TrRental.Add(rental);

                // Buat entri baru untuk pembayaran di TrPayment
                var payment = new TrPayment
                {
                    Payment_id = newPaymentId,
                    rental_id = rental.Rental_id,
                    payment_date = null, // Belum ada pembayaran
                    amount = totalPrice,
                    payment_method = "Belum Dibayar"
                };

                _context.TrPayment.Add(payment);

                await _context.SaveChangesAsync();
                return null; // Berhasil
            }
            catch (Exception ex)
            {
                return $"Error saat menyimpan data penyewaan: {ex.Message}";
            }
        }

    }
}
