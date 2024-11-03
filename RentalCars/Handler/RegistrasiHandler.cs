using RentalCars.Data;
using RentalCars.Models;

namespace RentalCars.Handlers
{
    public class RegistrasiHandler
    {
        private readonly AppDbContext _context;

        public RegistrasiHandler(AppDbContext context)
        {
            _context = context;
        }

        public bool isEmailRegistered(string email)
        {
            return _context.MsCustomer.Any(x => x.email == email);
        }

        //Add User Baru
        public bool RegisterUser(MsCustomer newUser)
        {
            //Ngambil Customer Id terakhir dari data
            var lastCustomer = _context.MsCustomer.OrderByDescending
                (x => x.Customer_id).FirstOrDefault();

            //Generate ID
            if (lastCustomer != null)
            {
                var lastId = int.Parse(lastCustomer.Customer_id.Substring(3));
                newUser.Customer_id = "CUS" + (lastId + 1).ToString("D3");

            }

            // Generate default driver_license_number jika tidak ada
            if (string.IsNullOrWhiteSpace(newUser.driver_license_number))
            {
                newUser.driver_license_number = "-";
            }


            // Jika blm ada customer
            else
            {
                newUser.Customer_id = "CUS001";
            }
            
            //add
            _context.MsCustomer.Add(newUser);

            //1 = true
            return _context.SaveChanges() > 0;
        }
    }
}
