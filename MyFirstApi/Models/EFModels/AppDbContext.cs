using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MyFirstApi.Models.EFModels
{
	public partial class AppDbContext : DbContext
	{
		public AppDbContext()
			: base("name=AppDbContext")
		{
		}

		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Member> Members { get; set; }
		public virtual DbSet<News> News { get; set; }
		public virtual DbSet<OrderItem> OrderItems { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Shipping> Shippings { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.Category)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.Property(e => e.Password)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<OrderItem>()
				.HasMany(e => e.ShippingDetails)
				.WithRequired(e => e.OrderItem)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.OrderItems)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.ShippingDetails)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Shipping>()
				.HasMany(e => e.ShippingDetails)
				.WithRequired(e => e.Shipping)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Shippings)
				.WithRequired(e => e.User)
				.HasForeignKey(e => e.CreatedBy)
				.WillCascadeOnDelete(false);
		}
	}
}
