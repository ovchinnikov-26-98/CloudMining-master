using CloudMining.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace CloudMining.DataContext
{
	public class BaseDataContext : DbContext
	{
		public DbSet<Member> Members { get; set; }
		public DbSet<Deposit> Deposits { get; set; }
		public DbSet<Currency> Currencies { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Purchase> Purchases { get; set; }
		public DbSet<PurchaseShare> PurchaseShares { get; set; }
		public DbSet<Payout> Payouts { get; set; }
		public DbSet<PayoutShare> PayoutShares { get; set; }
		public DbSet<ElectricityPayment> ElectricityPayments { get; set; }
		public DbSet<ElectricityPaymentShare> ElectricityPaymentShares { get; set; }

		public BaseDataContext()
		{
			/*Database.EnsureDeleted();
			Database.EnsureCreated();

			var r1 = new Role { Name = "Участник", Fee = 0 };
			var r2 = new Role { Name = "Администратор", Fee = 3 };
			var r3 = new Role { Name = "Менеджер", Fee = 5 };
			Roles.AddRange(r1, r2, r3);

			var c1 = new Currency { Name = "Bitcoin", ShortName = "BTC", Precision = 4 };
			var c2 = new Currency { Name = "Ethereum", ShortName = "ETH", Precision = 4 };
			var c3 = new Currency { Name = "Litecoin", ShortName = "LTC", Precision = 0 };
			var c4 = new Currency { Name = "Dogecoin", ShortName = "DOGE", Precision = 0 };
			Currencies.AddRange(c1, c2, c3, c4);

			SaveChanges();*/
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(
				ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
				new MySqlServerVersion(new Version(5, 7, 27)));      
		}
	}
}
