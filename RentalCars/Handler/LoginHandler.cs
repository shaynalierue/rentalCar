using RentalCars.Data;

namespace RentalCars.Handlers
{
    public class LoginHandler
    {
        //Data
        private readonly AppDbContext _context;

        public LoginHandler(AppDbContext context)
        {
            _context = context;
        }

        public bool ValidateUser(string email, string password)
        {
            // Mengambil user berdasarkan email
            var user = _context.MsCustomer.SingleOrDefault(u => u.email == email);

            // Jika user tidak ditemukan, kembalikan false
            if (user == null)
            {
                return false;
            }

            // Periksa kecocokan password
            return user.password == password;
        }


        public bool HandleLogin(string email, string password)
        {
            return ValidateUser(email, password);
        }
    }
}
