namespace RentalCars.Services
{
    public interface IUserService
    {
        bool ValidateUser(string email, string password);
    }
}
