using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using smartVenue.core.Entities.Identity;
using smartVenue.core.Enums;
using smartVenue.DataAccess.Context;
using smartVenue.core.Entities.Venues;
using smartVenue.core.Entites.Ticketing;
using smartVenue.core.Entites.Identity;


namespace smartVenue.Infrastructure.Persistence.Seed;

public static class DataSeeder
{
    public const string DemoPassword = "Demo@12345";

    private const int OccurrenceDays = 7;


    public static class Roles
    {
        public const string Customer = "Customer";
        public const string VenueOwner = "VenueOwner";
        public const string VenueEmployee = "VenueEmployee";
        public const string PlatformAdmin = "PlatformAdmin";

        public static readonly string[] All =
        {
            Customer,
            VenueOwner,
            VenueEmployee,
            PlatformAdmin
        };
    }

  

    private sealed record SeededUsers(
        ApplicationUser Admin,
        ApplicationUser PoolOwner,
        ApplicationUser CinemaOwner,
        ApplicationUser PendingOwner,
        ApplicationUser PoolEmployee,
        ApplicationUser CinemaEmployee,
        ApplicationUser Customer1,
        ApplicationUser Customer2
    );

    // ------------------------------------------------------------
    // All Days
    // ------------------------------------------------------------

    private static readonly WeekDays AllDays =
        WeekDays.Sunday |
        WeekDays.Monday |
        WeekDays.Tuesday |
        WeekDays.Wednesday |
        WeekDays.Thursday |
        WeekDays.Friday |
        WeekDays.Saturday;

    // ------------------------------------------------------------
    // Main Seed Method
    // ------------------------------------------------------------

    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var sp = scope.ServiceProvider;

        var db = sp.GetRequiredService<SmartVenueDbContext>();

        var userManager =
            sp.GetRequiredService<UserManager<ApplicationUser>>();

        var roleManager =
            sp.GetRequiredService<RoleManager<IdentityRole<int>>>();

        var logger =
            sp.GetRequiredService<ILoggerFactory>()
                .CreateLogger("DataSeeder");

        // Apply pending migrations.
        await db.Database.MigrateAsync();

        // Seed Identity data.
        await SeedRolesAsync(roleManager);

        var users = await SeedUsersAsync(userManager);

        // Seed venue-related data in one transaction.
        await using var transaction =
            await db.Database.BeginTransactionAsync();

        try
        {
            // Swimming Pool
            var pool = await db.Set<Venue>()
                .FirstOrDefaultAsync(x =>
                    x.Name == "Blue Wave Swimming Pool");

            if (pool is null)
            {
                pool = await SeedSwimmingPoolAsync(
                    db,
                    users.PoolOwner.Id);
            }

            // Cinema
            var cinema = await db.Set<Venue>()
                .FirstOrDefaultAsync(x =>
                    x.Name == "Star Cinema Hall");

            if (cinema is null)
            {
                cinema = await SeedCinemaAsync(
                    db,
                    users.CinemaOwner.Id);
            }

            // Employees
            await SeedEmployeesAsync(
                db,
                users,
                pool.Id,
                cinema.Id);

            // Join Requests
            await SeedJoinRequestsAsync(
                db,
                users,
                pool.Id);

            await transaction.CommitAsync();

            logger.LogInformation(
                "DataSeeder: seeding completed successfully.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            logger.LogError(
                ex,
                "DataSeeder: an error occurred while seeding data.");

            throw;
        }
    }

    // ============================================================
    // ROLES
    // ============================================================

    private static async Task SeedRolesAsync(
        RoleManager<IdentityRole<int>> roleManager)
    {
        foreach (var role in Roles.All)
        {
            if (await roleManager.RoleExistsAsync(role))
                continue;

            var result = await roleManager.CreateAsync(
                new IdentityRole<int>(role));

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Failed to create role {role}: " +
                    string.Join(
                        "; ",
                        result.Errors.Select(e => e.Description)));
            }
        }
    }

    // ============================================================
    // USERS
    // ============================================================

    private static async Task<SeededUsers> SeedUsersAsync(
        UserManager<ApplicationUser> userManager)
    {
        var admin = await EnsureUserAsync(
            userManager,
            "admin@smartvenue.test",
            "Platform Admin",
            "01000000001",
            Roles.PlatformAdmin);

        var poolOwner = await EnsureUserAsync(
            userManager,
            "pool.owner@smartvenue.test",
            "Pool Owner",
            "01000000002",
            Roles.VenueOwner);

        var cinemaOwner = await EnsureUserAsync(
            userManager,
            "cinema.owner@smartvenue.test",
            "Cinema Owner",
            "01000000003",
            Roles.VenueOwner);

        var pendingOwner = await EnsureUserAsync(
            userManager,
            "new.owner@smartvenue.test",
            "New Venue Owner",
            "01000000008",
            Roles.VenueOwner);

        var poolEmployee = await EnsureUserAsync(
            userManager,
            "pool.staff@smartvenue.test",
            "Pool Gate Staff",
            "01000000004",
            Roles.VenueEmployee);

        var cinemaEmployee = await EnsureUserAsync(
            userManager,
            "cinema.staff@smartvenue.test",
            "Cinema Gate Staff",
            "01000000005",
            Roles.VenueEmployee);

        var customer1 = await EnsureUserAsync(
            userManager,
            "customer1@smartvenue.test",
            "Ahmed Customer",
            "01000000006",
            Roles.Customer);

        var customer2 = await EnsureUserAsync(
            userManager,
            "customer2@smartvenue.test",
            "Sara Customer",
            "01000000007",
            Roles.Customer);

        return new SeededUsers(
            admin,
            poolOwner,
            cinemaOwner,
            pendingOwner,
            poolEmployee,
            cinemaEmployee,
            customer1,
            customer2);
    }

    // ------------------------------------------------------------
    // Ensure User
    // ------------------------------------------------------------

    private static async Task<ApplicationUser> EnsureUserAsync(
        UserManager<ApplicationUser> userManager,
        string email,
        string fullName,
        string phone,
        string role)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,

                FullName = fullName,
                PhoneNumber = phone,

                IsActive = true,

                CreatedAt = DateTime.UtcNow
            };

            var createResult = await userManager.CreateAsync(
                user,
                DemoPassword);

            if (!createResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Failed to create user {email}: " +
                    string.Join(
                        "; ",
                        createResult.Errors.Select(e => e.Description)));
            }
        }

        if (!await userManager.IsInRoleAsync(user, role))
        {
            var roleResult = await userManager.AddToRoleAsync(
                user,
                role);

            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Failed to add role {role} to {email}: " +
                    string.Join(
                        "; ",
                        roleResult.Errors.Select(e => e.Description)));
            }
        }

        return user;
    }

    // ============================================================
    // SWIMMING POOL
    // ============================================================

    private static async Task<Venue> SeedSwimmingPoolAsync(
        SmartVenueDbContext db,
        int ownerId)
    {
        var venue = new Venue
        {
            OwnerId = ownerId,

            Name = "Blue Wave Swimming Pool",

            Description =
                "Olympic-size indoor swimming pool with open swim sessions for adults and kids.",

            VenueType = VenueType.SwimmingPool,

            Address = "12 Nile Corniche St",
            City = "Giza",

            TotalCapacity = 100,

            Status = VenueStatus.Published,
            IsContracted = true,

            CreatedAt = DateTime.UtcNow
        };

        db.Add(venue);

        await db.SaveChangesAsync();

        // Images
        db.AddRange(
            new VenueImage
            {
                VenueId = venue.Id,
                Url = "/images/venues/pool-1.jpg",
                IsPrimary = true,
                SortOrder = 1
            },
            new VenueImage
            {
                VenueId = venue.Id,
                Url = "/images/venues/pool-2.jpg",
                IsPrimary = false,
                SortOrder = 2
            });

        // Policy
        db.Add(new VenuePolicy
        {
            VenueId = venue.Id,

            CancellationCutoffMinutes = 120,
            MaxPartySize = 6
        });

        // Offerings
        var openSwim = new VenueOffering
        {
            VenueId = venue.Id,

            Name = "Open Swim",
            Description = "General swimming access.",

            BasePrice = 50m,

            IsActive = true
        };

        var lessons = new VenueOffering
        {
            VenueId = venue.Id,

            Name = "Swimming Lesson",
            Description = "Group lesson with a certified coach.",

            BasePrice = 120m,

            IsActive = true
        };

        db.AddRange(openSwim, lessons);

        await db.SaveChangesAsync();

        // Sessions
        var morning = new VenueSession
        {
            VenueId = venue.Id,
            VenueOfferingId = openSwim.Id,

            Name = "Morning Swim",

            StartTime = new TimeOnly(8, 0),
            EndTime = new TimeOnly(10, 0),

            DaysOfWeek = AllDays,

            DefaultCapacity = 40,

            IsActive = true
        };

        var evening = new VenueSession
        {
            VenueId = venue.Id,
            VenueOfferingId = openSwim.Id,

            Name = "Evening Swim",

            StartTime = new TimeOnly(17, 0),
            EndTime = new TimeOnly(19, 0),

            DaysOfWeek = AllDays,

            DefaultCapacity = 40,

            IsActive = true
        };

        var lesson = new VenueSession
        {
            VenueId = venue.Id,
            VenueOfferingId = lessons.Id,

            Name = "Kids Lesson",

            StartTime = new TimeOnly(15, 0),
            EndTime = new TimeOnly(16, 0),

            DaysOfWeek =
                WeekDays.Saturday |
                WeekDays.Monday |
                WeekDays.Wednesday,

            DefaultCapacity = 15,

            IsActive = true
        };

        db.AddRange(morning, evening, lesson);

        await db.SaveChangesAsync();

        // Ticket Types
        db.AddRange(
            new SessionTicketType
            {
                VenueSessionId = morning.Id,
                Name = "Adult",
                Price = 50m,
                IsActive = true
            },
            new SessionTicketType
            {
                VenueSessionId = morning.Id,
                Name = "Child",
                Price = 30m,
                IsActive = true
            },
            new SessionTicketType
            {
                VenueSessionId = evening.Id,
                Name = "Adult",
                Price = 60m,
                IsActive = true
            },
            new SessionTicketType
            {
                VenueSessionId = evening.Id,
                Name = "Child",
                Price = 35m,
                IsActive = true
            },
            new SessionTicketType
            {
                VenueSessionId = lesson.Id,
                Name = "Lesson Seat",
                Price = 120m,
                IsActive = true
            });

        await db.SaveChangesAsync();

        // Occurrences
        await AddOccurrencesAsync(
            db,
            venue.Id,
            new[] { morning, evening, lesson });

        return venue;
    }

    // ============================================================
    // CINEMA
    // ============================================================

    private static async Task<Venue> SeedCinemaAsync(
        SmartVenueDbContext db,
        int ownerId)
    {
        var venue = new Venue
        {
            OwnerId = ownerId,

            Name = "Star Cinema Hall",

            Description =
                "Modern cinema hall with standard and VIP screenings.",

            VenueType = VenueType.Cinema,

            Address = "45 Pyramids Road",
            City = "Giza",

            TotalCapacity = 200,

            Status = VenueStatus.Published,
            IsContracted = true,

            CreatedAt = DateTime.UtcNow
        };

        db.Add(venue);

        await db.SaveChangesAsync();

        // Images
        db.AddRange(
            new VenueImage
            {
                VenueId = venue.Id,
                Url = "/images/venues/cinema-1.jpg",
                IsPrimary = true,
                SortOrder = 1
            },
            new VenueImage
            {
                VenueId = venue.Id,
                Url = "/images/venues/cinema-2.jpg",
                IsPrimary = false,
                SortOrder = 2
            });

        // Policy
        db.Add(new VenuePolicy
        {
            VenueId = venue.Id,

            CancellationCutoffMinutes = 60,
            MaxPartySize = 10
        });

        // Offerings
        var standard = new VenueOffering
        {
            VenueId = venue.Id,

            Name = "Standard Screening",
            Description = "Regular hall screening.",

            BasePrice = 80m,

            IsActive = true
        };

        var vip = new VenueOffering
        {
            VenueId = venue.Id,

            Name = "VIP Screening",
            Description = "Recliner seats with premium service.",

            BasePrice = 180m,

            IsActive = true
        };

        db.AddRange(standard, vip);

        await db.SaveChangesAsync();

        // Sessions
        var matinee = new VenueSession
        {
            VenueId = venue.Id,
            VenueOfferingId = standard.Id,

            Name = "Matinee Show",

            StartTime = new TimeOnly(14, 0),
            EndTime = new TimeOnly(16, 0),

            DaysOfWeek = AllDays,

            DefaultCapacity = 120,

            IsActive = true
        };

        var night = new VenueSession
        {
            VenueId = venue.Id,
            VenueOfferingId = standard.Id,

            Name = "Night Show",

            StartTime = new TimeOnly(20, 0),
            EndTime = new TimeOnly(22, 30),

            DaysOfWeek = AllDays,

            DefaultCapacity = 150,

            IsActive = true
        };

        var vipNight = new VenueSession
        {
            VenueId = venue.Id,
            VenueOfferingId = vip.Id,

            Name = "VIP Late Show",

            StartTime = new TimeOnly(22, 45),
            EndTime = new TimeOnly(23, 59),

            DaysOfWeek =
                WeekDays.Thursday |
                WeekDays.Friday |
                WeekDays.Saturday,

            DefaultCapacity = 40,

            IsActive = true
        };

        db.AddRange(matinee, night, vipNight);

        await db.SaveChangesAsync();

        // Ticket Types
        db.AddRange(
            new SessionTicketType
            {
                VenueSessionId = matinee.Id,
                Name = "Standard",
                Price = 80m,
                IsActive = true
            },
            new SessionTicketType
            {
                VenueSessionId = matinee.Id,
                Name = "Student",
                Price = 60m,
                IsActive = true
            },
            new SessionTicketType
            {
                VenueSessionId = night.Id,
                Name = "Standard",
                Price = 100m,
                IsActive = true
            },
            new SessionTicketType
            {
                VenueSessionId = night.Id,
                Name = "Student",
                Price = 75m,
                IsActive = true
            },
            new SessionTicketType
            {
                VenueSessionId = vipNight.Id,
                Name = "VIP",
                Price = 180m,
                IsActive = true
            });

        await db.SaveChangesAsync();

        // Occurrences
        await AddOccurrencesAsync(
            db,
            venue.Id,
            new[] { matinee, night, vipNight });

        return venue;
    }

    // ============================================================
    // EMPLOYEES
    // ============================================================

    private static async Task SeedEmployeesAsync(
        SmartVenueDbContext db,
        SeededUsers users,
        int poolId,
        int cinemaId)
    {
        var employees = new[]
        {
            new VenueEmployee
            {
                UserId = users.PoolEmployee.Id,
                VenueId = poolId,
                IsActive = true
            },

            new VenueEmployee
            {
                UserId = users.CinemaEmployee.Id,
                VenueId = cinemaId,
                IsActive = true
            }
        };

        foreach (var employee in employees)
        {
            var exists = await db.Set<VenueEmployee>()
                .AnyAsync(x =>
                    x.UserId == employee.UserId &&
                    x.VenueId == employee.VenueId);

            if (!exists)
            {
                db.Add(employee);
            }
        }

        await db.SaveChangesAsync();
    }

    // ============================================================
    // VENUE JOIN REQUESTS
    // ============================================================

    private static async Task SeedJoinRequestsAsync(
        SmartVenueDbContext db,
        SeededUsers users,
        int poolId)
    {
        // Approved Request
        var approvedExists = await db.Set<VenueJoinRequest>()
            .AnyAsync(x =>
                x.RequesterId == users.PoolOwner.Id &&
                x.Status == VenueJoinRequestStatus.Approved);

        if (!approvedExists)
        {
            db.Add(new VenueJoinRequest
            {
                RequesterId = users.PoolOwner.Id,

                VenueName = "Blue Wave Swimming Pool",
                VenueType = VenueType.SwimmingPool,

                Address = "12 Nile Corniche St",
                City = "Giza",

                ApproxCapacity = 100,

                Description = "Indoor Olympic-size pool.",

                ContactPhone = "01000000002",

                WebsiteUrl = null,

                Status = VenueJoinRequestStatus.Approved,

                ReviewedByUserId = users.Admin.Id,

                ReviewNote = "Documents verified.",

                CreatedVenueId = poolId,

                CreatedAt = DateTime.UtcNow.AddDays(-10),

                ReviewedAt = DateTime.UtcNow.AddDays(-9)
            });
        }

        // Pending Request
        var pendingExists = await db.Set<VenueJoinRequest>()
            .AnyAsync(x =>
                x.RequesterId == users.PendingOwner.Id &&
                x.Status == VenueJoinRequestStatus.PendingReview);

        if (!pendingExists)
        {
            db.Add(new VenueJoinRequest
            {
                RequesterId = users.PendingOwner.Id,

                VenueName = "Sunrise Fitness Pool",
                VenueType = VenueType.SwimmingPool,

                Address = "8 Tahrir Square",
                City = "Cairo",

                ApproxCapacity = 60,

                Description =
                    "Small community pool looking to join the platform.",

                ContactPhone = "01000000008",

                WebsiteUrl = "https://example.com/sunrise",

                Status = VenueJoinRequestStatus.PendingReview,

                CreatedAt = DateTime.UtcNow.AddDays(-1)
            });
        }

        await db.SaveChangesAsync();
    }

    // ============================================================
    // SESSION OCCURRENCES
    // ============================================================

    private static async Task AddOccurrencesAsync(
        SmartVenueDbContext db,
        int venueId,
        IEnumerable<VenueSession> sessions)
    {
        var today = DateTime.UtcNow.Date;

        var occurrences = new List<SessionOccurrence>();

        foreach (var session in sessions)
        {
            for (var i = 0; i < OccurrenceDays; i++)
            {
                var date = today.AddDays(i);

                var day = (WeekDays)Enum.Parse(
                    typeof(WeekDays),
                    date.DayOfWeek.ToString());

                if (!session.DaysOfWeek.HasFlag(day))
                    continue;

                occurrences.Add(new SessionOccurrence
                {
                    VenueId = venueId,

                    VenueSessionId = session.Id,

                    StartAt = date.Add(
                        session.StartTime.ToTimeSpan()),

                    EndAt = date.Add(
                        session.EndTime.ToTimeSpan()),

                    Capacity = session.DefaultCapacity,

                    ReservedCount = 0,

                    Status = SessionOccurrenceStatus.Open
                });
            }
        }

        if (occurrences.Count > 0)
        {
            db.AddRange(occurrences);

            await db.SaveChangesAsync();
        }
    }
}