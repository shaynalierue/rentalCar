using RentalCars.Data;
using RentalCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentalCars.Handlers
{
    public class HistoryRentHandler
    {
        private readonly AppDbContext _context;

        public HistoryRentHandler(AppDbContext context)
        {
            _context = context;
        }

        public List<CarRentalHistory> GetRentalHistory(string customerName)
        {
            Console.WriteLine("Nama pelanggan yang dicari: " + customerName);

            var result = (from rental in _context.TrRental
                          join car in _context.MsCar on rental.car_id equals car.Car_id
                          join customer in _context.MsCustomer on rental.customer_id equals customer.Customer_id
                          where customer.email == customerName
                          select new CarRentalHistory
                          {
                              RentalDate = rental.rental_date,
                              CarName = car.name,
                              PricePerDay = car.price_per_day,
                              TotalDays = (int)(rental.return_date - rental.rental_date).Value.TotalDays + 1,
                              TotalPrice = rental.total_price,
                              PaymentStatus = rental.payment_status == true ? "Sudah Dibayar" : "Belum Dibayar"
                          }).ToList();

            Console.WriteLine("Jumlah riwayat yang ditemukan: " + result.Count);

            return result;
        }

    }
}
