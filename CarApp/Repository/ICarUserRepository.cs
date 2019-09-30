using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarApp.Entities;

namespace CarApp.Repository
{
    public interface ICarUserRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();

        // Dishes
        IEnumerable<Car> GetCars();
        IEnumerable<Car> GetCarByType(string carType);

        Car GetCar(int id);
        User GetUserWithCars(int id);

     
      
        User GetUser(int userId);
    }
}
  