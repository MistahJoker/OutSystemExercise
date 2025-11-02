using System.Data.Common;
using EventBookingSystem.DBAccess.Interfaces;
using EventBookingSystem.Entities;

namespace EventBookingSystem.DBAccess.MockUp
{
    public class MockUpUser : IUserRepository
    {
        private readonly MockUpDB _db;

        // Constructor to initialize with some mock data
        public MockUpUser(MockUpDB db)
        {
            _db = db;
        }
        public List<User> GetAllUsers()
        {
            return _db.Users;
        }
        public List<User> GetAllUsersIncludingBookings()
        {
            List<User> allUsers = _db.Users;
            foreach (var user in allUsers)
            {
                user.Bookings = _db.Bookings.Where(b => b.User!=null && b.User.UserId == user.UserId).ToList();
            }
            return allUsers;
        }
        public User? GetUserById(int userId)
        {
            if (userId > 0)
            {
                throw new ArgumentException(nameof(userId));
            }

            return _db.Users.FirstOrDefault(u => u.UserId == userId);
        }
        
        public User? GetUserByIdIncludingBookings(int userId)
        {
            if (userId > 0)
            {
                throw new ArgumentException(nameof(userId));
            }

            var user = _db.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                user.Bookings = _db.Bookings.Where(b => b.User != null && b.User.UserId == userId).ToList();
            }
            return user;
        }
        public User AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            //simulate auto-incrementing primary key
            int newUserId = _db.Users.Any() ? _db.Users.Max(u => u.UserId) + 1 : 1;
            user.UserId = newUserId;
            _db.Users.Add(user);
            return user;
        }
        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var existingUser = GetUserById(user.UserId);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with UserId {user.UserId} does not exist.");
            }

            //update properties as needed
        }
        public void DeleteUser(int userId)
        {
            if (userId > 0)
            {
                throw new ArgumentException(nameof(userId));
            }
            var existingUser = _db.Users.FirstOrDefault(u => u.UserId == userId);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with UserId {userId} does not exist.");
            }

            _db.Users.Remove(existingUser);

        }
        
    }
}