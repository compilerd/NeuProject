using System.Threading.Tasks;


public class CarUserInitializer
{
    private CarUserContext _ctx;
    private UserManager<User> _userMgr;
    private RoleManager<IdentityRole> _roleMgr;

    public CarUserInitializer(UserManager<User> userMgr,
                          RoleManager<IdentityRole> roleMgr,
                          CarUserContext ctx)
    {
        _ctx = ctx;
        _userMgr = userMgr;
        _roleMgr = roleMgr;
    }

    public async Task Seed()
    {

        var user = await _userMgr.FindByNameAsync("ayush");

        if (user == null)
        {
            if (!(await _roleMgr.RoleExistsAsync("Admin")))
            {
                var role = new IdentityRole("Admin");
                role.Claims.Add(new IdentityRoleClaim<string>()
                {
                    ClaimType = "IsAdmin",
                    ClaimValue = "True"
                });
                await _roleMgr.CreateAsync(role);
            }
            user = new User()
            {
                UserName = "ayush",
                FirstName = "Ayush",
                LastName = "Sinha",
                Email = "ayush@gmail.com",
                Password = "test"
            };

            var userResult = await _userMgr.CreateAsync(user, "ayush@!");
            var roleResult = await _userMgr.AddToRoleAsync(user, "Admin");
            var claimResult = await _userMgr.AddClaimAsync(user, new Claim("SuperUser", "True"));

            if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to build user or role");
            }
        }

        if (!_ctx.Cars.Any())
        {
            _ctx.AddRange(_sample);
            await _ctx.SaveChangesAsync();
        }
    }
    List<Car> _sample = new List<Car>
      {
                  new Car()
                  {
                      CarName = "Duster",
                      CarType = "SUV",

                      ImageUrl = "/images/vadonut.jpg"
                  },

     };
    };
