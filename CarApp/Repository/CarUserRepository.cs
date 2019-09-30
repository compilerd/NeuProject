using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarApp.Entities;

using CarApp.Repository;

using Microsoft.EntityFrameworkCore;

namespace CarApp.Data
{
    public class CarUserRepository : ICarUserRepository
    {
        private readonly CarUserContext _context;
        public CarUserRepository(CarUserContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public IEnumerable<Car> GetCars()
        {
            return _context.Cars.ToList();
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public User GetUserWithCars(int id)
        {
            return _context.Users
                .Include(d => d.Car)
                .Where(d => d.UserId == id)
                .FirstOrDefault();
        }
        public Car GetCar(int id)
        {
            return _context.Cars
                .Where(d => d.CarId == id)
                .FirstOrDefault();
        }
        public async Task<bool> SaveAllAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public IEnumerable<Car> GetCarByCarType(string carType)
        {
            return _context.Cars
                  .Where(c => c.CarType.Equals(carType, StringComparison.CurrentCultureIgnoreCase))
                  .OrderBy(d => d.CarName)
                 .ToList();
        }

        public User GetUser(int userId)
        {
            return _context.Users
                 .Include(u => u.Claims)
                 .Include(u => u.Roles)
                 .Where(u => u.UserId == userId)
                 .Cast<User>()
                 .FirstOrDefault();
        }

        public IEnumerable<Car> GetCarByType(string cartype)
        {
            throw new NotImplementedException();
        }
    }
}


