using LabsDB.Entity;
using Microsoft.EntityFrameworkCore;

namespace MainApp;

public class ApplicationContext : DbContext
{
    private readonly bool _test;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        _test = false;
        Database.Migrate();
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options, bool test) : base(options)
    {
        _test = test;
    }

    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Room> Room { get; set; } = null!;
    public DbSet<Lamp> Lamp { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (_test)
        {
            base.OnModelCreating(modelBuilder);
            return;
        }

        var emp = new Employee {Id = 1, Login = "123", Password = "123"};
        var houses = new List<Room>(new[]
        {
            new Room {Id = 1},
            new Room {Id = 2}
        });
        var inds = new List<object>(new[]
        {
            new {Id = 1, Glows = true, TimeStamp = DateTime.Now, RoomId = 1},
            new {Id = 2, Glows = true, TimeStamp = DateTime.Now, RoomId = 1},
            new {Id = 3, Glows = true, TimeStamp = DateTime.Now, RoomId = 2},
            new {Id = 4, Glows = false, TimeStamp = DateTime.Now, RoomId = 2}

        });
        modelBuilder.Entity<Employee>().HasData(emp);
        modelBuilder.Entity<Room>().HasData(houses);
        modelBuilder.Entity<Lamp>(i =>
        {
            i.HasOne(ind => ind.Room)
                .WithMany(e => e.Lamps)
                .HasForeignKey(ind => ind.RoomId);
            i.HasData(inds);
        });
        base.OnModelCreating(modelBuilder);
    }
}