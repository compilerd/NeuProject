public class CarUserContext: IdentityDbContext
{
    private IConfigurationRoot _config;
    public CarUserContext(DbContextOptions<CarUserContext> options, IConfigurationRoot config)
    : base("NeuCarDatabase")
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

