
using FabricProject.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Sql;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FabricProject.Models.Security;

namespace FabricProject.DContext
{
    public class FabricProjectDbContext : IdentityDbContext<FbUser, FbRole, int, FbUserClaim, FbUserRole
                                                     , FbUserLogin, FbRoleClaim, FbUserToken>

    {
        #region Properties
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Materail> Materials { get; set; }

        public DbSet<Lab> Labs { get; set; }

        public DbSet<Cloth> Cloths { get; set; }

        public DbSet<Deliver> Delivers { get; set; }

        public DbSet<Machine> Machines { get; set; }

        public DbSet<MachineOrder> MachineOrders { get; set; }

        public DbSet<CustomerOrderDetailsMachine> CustomerOrderDetailsMachine { get; set; }

        public DbSet<MaterialOrderDetailCustomer> MaterialOrderDetailCustomers { get; set; }

        public DbSet<CustomerOrderDetail> OrderDetailCustomers { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<CustomerOrder> CustomerOrders { get; set; }

        public DbSet<ClothMaterail> ClothMaterails { get; set; }
        #endregion


        #region Constructor
        public FabricProjectDbContext
        (DbContextOptions<FabricProjectDbContext> options)
        : base(options)
        {
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedData(modelBuilder);
        }

        

        #region SeedData

        private void SeedData(ModelBuilder modelBuilder)
        {

            string[] Name = new string[5];
            string[] NormalizedName = new string[5];
            string[] NameAr = new string[5];
            Name[0] = "Admin"; NormalizedName[0] = "ADMIN"; NameAr[0] = "مدير عام";
            Name[1] = "Receiving"; NormalizedName[1] = "RECEIVING"; NameAr[1] = "أمين مستودع الاستلام";
            Name[2] = "Delivery"; NormalizedName[2] = "DELIVERY"; NameAr[2] = "مسؤول التسليم";
            Name[3] = "Machine"; NormalizedName[3] = "MACHINE"; NameAr[3] = "مسؤول الماكينات";
            Name[4] = "Laboratory"; NormalizedName[4] = "LABORATORY"; NameAr[4] = "مسؤول المخبر";

            for (int i = 0; i < 5; i++)
            {
                modelBuilder.Entity<FbRole>().HasData(new FbRole
                {
                    Id = i + 1,
                    Name = Name[i],
                    NormalizedName = NormalizedName[i],
                    NameAr = NameAr[i]
                });
            }
        }
        #endregion



    }
}
