using Microsoft.Data.SqlClient;
using RentalCars.Controllers;
using RentalCars.Data;
using RentalCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentalCars.Handlers
{
    public class HomeHandler
    {
        private readonly AppDbContext _context;

        public HomeHandler(AppDbContext context)
        {
            _context = context;
        }
        public (List<CarWithImage>, int) GetAvailableCars(DateTime pickup, DateTime returns, int? tahun, int page, int pageSize, string sortOrder)
        {
            //var rentalDays = (returns - pickup).Days;
            int rentalDays = (int)(returns.Date - pickup.Date).TotalDays + 1;

            // Ambil mobil yang tersedia dan belum disewa
            var cars = _context.MsCar
                .Where(car => car.status == true && !_context.TrRental.Any(rental => rental.car_id == car.Car_id))
                .Select(car => new CarWithImage
                {
                    CarId = car.Car_id,
                    Name = car.name,
                    Price = car.price_per_day,
                    Year = car.year,
                    Transmission = car.transmission,
                    PassengerCapacity = car.number_of_car_seats,
                    Image = _context.MsCarImages
                .Where(img => img.Car_id == car.Car_id)
                .Select(img => img.image_link)
                .FirstOrDefault() ?? "default.jpg", // Set gambar default atau tambahkan sesuai kebutuhan
                    TotalPrice = car.price_per_day * rentalDays
                });

            // Filter berdasarkan tahun
            if (tahun.HasValue)
            {
                cars = cars.Where(car => car.Year == tahun);
            }

            // Sorting
            cars = sortOrder == "asc"
                ? cars.OrderBy(car => car.Price)
                : sortOrder == "desc"
                ? cars.OrderByDescending(car => car.Price)
                : cars;

            // Menghitung total data untuk paginasi
            int totalCars = cars.Count();
            int totalPages = (int)Math.Ceiling(totalCars / (double)pageSize);

            // Mendapatkan data sesuai halaman
            List<CarWithImage> results = cars
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (results, totalPages);
        }

        public List<int?> GetAvailableYears()
        {
            return _context.MsCar
                .Where(car => car.year.HasValue)
                .Select(car => car.year)
                .Distinct()
                .OrderByDescending(year => year)
                .ToList();
        }

        public void UpdateCarImages()
        {
            // Ambil semua data MsCarImages
            var carImages = _context.MsCarImages.ToList();

            // Daftar nama file gambar
            string[] imageFiles = { "car1.jpg", "car2.jpg", "car3.jpg" };

            for (int i = 0; i < carImages.Count; i++)
            {
                // Asumsikan Anda ingin mengatur gambar secara berurutan
                int imageIndex = i % imageFiles.Length; // Mengulang gambar jika lebih dari 3
                carImages[i].image_link = $"img/{imageFiles[imageIndex]}"; // Set image link

                _context.MsCarImages.Update(carImages[i]); // Tandai entitas untuk diperbarui
            }

            // Simpan semua perubahan
            _context.SaveChanges();
        }


    }
}
