using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MELLBankRestAPI.AuthModels
{
    public class AuthenticationContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public AuthenticationContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var roleId_1 = Guid.NewGuid().ToString();
            var roleId_2 = Guid.NewGuid().ToString();
            var roleId_3 = Guid.NewGuid().ToString();

            // Seed Roles Data
            modelBuilder.Entity<IdentityRole>().HasData(
                new { Id = roleId_1, Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
                new { Id = roleId_2, Name = "BankStaff", NormalizedName = "BANKSTAFF" },
                new { Id = roleId_3, Name = "Customer", NormalizedName = "CUSTOMER" }
            );


            //Create Administrator
            var userId_1 = Guid.NewGuid().ToString();

            var adminUser = new ApplicationUser()
            {
                Id = userId_1,
                Email = "Sipho.Zwane@mellbank.com",
                EmailConfirmed = true,
                FirstName = "Sipho",
                LastName = "Zwane",
                UserName = "SiphoAdmin",
                NormalizedUserName = "SIPHOADMIN",
            };

            PasswordHasher<ApplicationUser> aPh = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = aPh.HashPassword(adminUser, "JT4s9VEo*q");

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_1,
                UserId = userId_1
            });


            //Create Bank Staff
            var userId_2 = Guid.NewGuid().ToString();
            var managerUser = new ApplicationUser()
            {
                Id = userId_2,
                Email = "Sindi.Ntuli@mellbank.com",
                EmailConfirmed = true,
                FirstName = "Sindi",
                LastName = "Ntuli",
                UserName = "SindiBankStaff",
                NormalizedUserName = "SINDIBANKSTAFF",
            };

            managerUser.PasswordHash = aPh.HashPassword(managerUser, "U*tsY4$d8e");

            modelBuilder.Entity<ApplicationUser>().HasData(managerUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_2,
                UserId = userId_2
            });


            // Create Customer Jabu Nkosi
            var userId_3 = Guid.NewGuid().ToString();
            var customerUser1 = new ApplicationUser()
            {
                Id = userId_3,
                Email = "Jabu.Nkosi@gmail.com",
                EmailConfirmed = true,
                FirstName = "Jabu",
                LastName = "Nkosi",
                UserName = "JabuNkosi",
                NormalizedUserName = "JABUNKOSI",
            };

            customerUser1.PasswordHash = aPh.HashPassword(customerUser1, "z*EH5x*h4A");
            modelBuilder.Entity<ApplicationUser>().HasData(customerUser1);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_3
            });

            // Create Customer Lebo Mokoena
            var userId_4 = Guid.NewGuid().ToString();
            var customerUser2 = new ApplicationUser()
            {
                Id = userId_4,
                Email = "lebo.mokoena@gmail.com",
                EmailConfirmed = true,
                FirstName = "Lebo",
                LastName = "Mokoena",
                UserName = "LeboMokoena",
                NormalizedUserName = "LEBOMOKOENA",
            };
            customerUser2.PasswordHash = aPh.HashPassword(customerUser2, "z*EH5x*h4A");
            modelBuilder.Entity<ApplicationUser>().HasData(customerUser2);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_4
            });

            // Create Customer Mpho Khumalo
            var userId_5 = Guid.NewGuid().ToString();
            var customerUser3 = new ApplicationUser()
            {
                Id = userId_5,
                Email = "mpho.ngwenya@gmail.com",
                EmailConfirmed = true,
                FirstName = "Mpho",
                LastName = "Khumalo",
                UserName = "MphoKhumalo",
                NormalizedUserName = "MPHOKHUMALO",
            };
            customerUser3.PasswordHash = aPh.HashPassword(customerUser3, "z*EH5x*h4A");
            modelBuilder.Entity<ApplicationUser>().HasData(customerUser3);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_5
            });

            // Create Customer Sizwe Dlamini
            var userId_6 = Guid.NewGuid().ToString();
            var customerUser4 = new ApplicationUser()
            {
                Id = userId_6,
                Email = "sizwe.dlamini@gmail.com",
                EmailConfirmed = true,
                FirstName = "Sizwe",
                LastName = "Dlamini",
                UserName = "SizweDlamini",
                NormalizedUserName = "SIZWEDLAMINI",
            };
            customerUser4.PasswordHash = aPh.HashPassword(customerUser4, "z*EH5x*h4A");
            modelBuilder.Entity<ApplicationUser>().HasData(customerUser4);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_6
            });

            // Create Customer Nomsa Mkhize
            var userId_7 = Guid.NewGuid().ToString();
            var customerUser5 = new ApplicationUser()
            {
                Id = userId_7,
                Email = "nomsa.mkhize@gmail.com",
                EmailConfirmed = true,
                FirstName = "Nomsa",
                LastName = "Mkhize",
                UserName = "NomsaMkhize",
                NormalizedUserName = "NOMSAMKHIZE",
            };
            customerUser5.PasswordHash = aPh.HashPassword(customerUser5, "z*EH5x*h4A");
            modelBuilder.Entity<ApplicationUser>().HasData(customerUser5);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_7
            });

            // Create Customer Mandla Zulu
            var userId_8 = Guid.NewGuid().ToString();
            var customerUser6 = new ApplicationUser()
            {
                Id = userId_8,
                Email = "mandla.zulu@gmail.com",
                EmailConfirmed = true,
                FirstName = "Mandla",
                LastName = "Zulu",
                UserName = "MandlaZulu",
                NormalizedUserName = "MANDLAZULU",
            };
            customerUser6.PasswordHash = aPh.HashPassword(customerUser6, "z*EH5x*h4A");
            modelBuilder.Entity<ApplicationUser>().HasData(customerUser6);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_8
            });

            // Create Customer Thando Maseko
            var userId_9 = Guid.NewGuid().ToString();
            var customerUser7 = new ApplicationUser()
            {
                Id = userId_9,
                Email = "thando.maseko@gmail.com",
                EmailConfirmed = true,
                FirstName = "Thando",
                LastName = "Maseko",
                UserName = "ThandoMaseko",
                NormalizedUserName = "THANDOMASEKO",
            };
            customerUser7.PasswordHash = aPh.HashPassword(customerUser7, "z*EH5x*h4A");
            modelBuilder.Entity<ApplicationUser>().HasData(customerUser7);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_9
            });

            // Create Customer Siyabonga Nkosi
            var userId_10 = Guid.NewGuid().ToString();
            var customerUser8 = new ApplicationUser()
            {
                Id = userId_10,
                Email = "siyabonga.nkosi@gmail.com",
                EmailConfirmed = true,
                FirstName = "Siyabonga",
                LastName = "Nkosi",
                UserName = "SiyabongaNkosi",
                NormalizedUserName = "SIYABONGANKOSI",
            };
            customerUser8.PasswordHash = aPh.HashPassword(customerUser8, "z*EH5x*h4A");
            modelBuilder.Entity<ApplicationUser>().HasData(customerUser8);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_10
            });

            // Create Customer Thuli Mthembu
            var userId_11 = Guid.NewGuid().ToString();
            var customerUser9 = new ApplicationUser()
            {
                Id = userId_11,
                Email = "thuli.mthembu@gmail.com",
                EmailConfirmed = true,
                FirstName = "Thuli",
                LastName = "Mthembu",
                UserName = "ThuliMthembu",
                NormalizedUserName = "THULIMTHEMBU",
            };
            customerUser9.PasswordHash = aPh.HashPassword(customerUser9, "z*EH5x*h4A");
            modelBuilder.Entity<ApplicationUser>().HasData(customerUser9);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_11
            });

            // Create Customer Sibongile Khumalo
            var userId_12 = Guid.NewGuid().ToString();
            var customerUser10 = new ApplicationUser()
            {
                Id = userId_12,
                Email = "sibongile.khumalo@gmail.com",
                EmailConfirmed = true,
                FirstName = "Sibongile",
                LastName = "Khumalo",
                UserName = "SibongileKhumalo",
                NormalizedUserName = "SIBONGILEKHUMALO",
            };
            customerUser10.PasswordHash = aPh.HashPassword(customerUser10, "z*EH5x*h4A");
            modelBuilder.Entity<ApplicationUser>().HasData(customerUser10);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_12
            });
        }
    }
}
