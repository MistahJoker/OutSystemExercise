using EventBookingSystem.Entities;

namespace EventBookingSystem.DBAccess.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        List<User> GetAllUsersIncludingBookings();
        User? GetUserById(int userId);
        User? GetUserByIdIncludingBookings(int userId);
        User AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
    }
}