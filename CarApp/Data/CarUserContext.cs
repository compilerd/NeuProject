using CarApp.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarApp.Data
{
    
        public class CarUserContext : IdentityDbContext
        {
            private IConfigurationRoot _config;
            public CarUserContext(DbContextOptions<CarUserContext> options, IConfigurationRoot config)
            : base(options)
            {
                _config = config;
            }
            public DbSet<User> Users { get; set; }
            public DbSet<Car> Cars { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseSqlServer(_config["Data:ConnectionString"]);
            }
        }
    }
