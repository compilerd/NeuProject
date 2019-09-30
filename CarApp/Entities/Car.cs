using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarApp.Entities
{
    public class Car
    {
       
            public int CarId { get; set; }
            public string CarName { get; set; }
            public string CarType { get; set; }
            public string ImageUrl { get; set; }


            public User User { get; set; }

        

    }
}
