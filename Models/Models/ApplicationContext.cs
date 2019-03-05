using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using ConfigurationClass;

namespace Models
{
	public partial class ApplicationContext : DbContext
	{		
		public ApplicationContext()
		{}

		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{}

		public virtual DbSet<Address> Address { get; set; }
		public virtual DbSet<City> City { get; set; }
		public virtual DbSet<CityDistricts> CityDistricts { get; set; }
		public virtual DbSet<Feedback> Feedback { get; set; }
		public virtual DbSet<OrderTable> OrderTable { get; set; }
		public virtual DbSet<OrderToService> OrderToService { get; set; }
		public virtual DbSet<Orderslist> Orderslist { get; set; }
		public virtual DbSet<ProfCategory> ProfCategory { get; set; }
		public virtual DbSet<Profession> Profession { get; set; }
		public virtual DbSet<Service> Service { get; set; }
		public virtual DbSet<Worker> Worker { get; set; }
		public virtual DbSet<User> User { get; set; }
		public virtual DbSet<Photo> Photo { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseNpgsql(ConfigurationClass.ConfigurationClass.GetConnectionString());
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

			modelBuilder.Entity<Address>(entity =>
			{
				entity.ToTable("address");

				entity.HasIndex(e => e.IdCity)
					.HasName("citytoaddress");

				entity.HasIndex(e => e.IdCityDistrict)
					.HasName("citydistrtoaddress");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.ApptNum)
					.IsRequired()
					.HasColumnName("appt_num")
					.HasMaxLength(5);

				entity.Property(e => e.IdCity).HasColumnName("id_city");

				entity.Property(e => e.IdCityDistrict).HasColumnName("id_city_district");

				entity.Property(e => e.StreetName)
					.IsRequired()
					.HasColumnName("street_name")
					.HasMaxLength(45);

				entity.HasOne(d => d.IdCityNavigation)
					.WithMany(p => p.Address)
					.HasForeignKey(d => d.IdCity)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("address_id_city_fkey");

				entity.HasOne(d => d.IdCityDistrictNavigation)
					.WithMany(p => p.Address)
					.HasForeignKey(d => d.IdCityDistrict)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("address_id_city_district_fkey");
			});

			modelBuilder.Entity<City>(entity =>
			{
				entity.ToTable("city");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasMaxLength(30);
			});

			modelBuilder.Entity<CityDistricts>(entity =>
			{
				entity.ToTable("city_districts");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.IdCity).HasColumnName("id_city");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasMaxLength(30);

				entity.HasOne(d => d.IdCityNavigation)
					.WithMany(p => p.CityDistricts)
					.HasForeignKey(d => d.IdCity)
					.HasConstraintName("city_districts_id_city_fkey");
			});

			modelBuilder.Entity<Feedback>(entity =>
			{
				entity.ToTable("feedback");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Date)
					.HasColumnName("date")
					.HasColumnType("date");

				entity.Property(e => e.GradeValue).HasColumnName("grade_value");

				entity.Property(e => e.IdWorker).HasColumnName("id_worker");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasMaxLength(30);

				entity.Property(e => e.Patronymic)
					.IsRequired()
					.HasColumnName("patronymic")
					.HasMaxLength(30);

				entity.Property(e => e.Text)
					.HasColumnName("text")
					.HasMaxLength(500);

				entity.HasOne(d => d.IdWorkerNavigation)
					.WithMany(p => p.Feedback)
					.HasForeignKey(d => d.IdWorker)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("feedback_id_worker_fkey");
			});

			modelBuilder.Entity<OrderTable>(entity =>
			{
				entity.ToTable("order_table");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.IdOrderList).HasColumnName("id_order_list");

				entity.Property(e => e.Info)
					.IsRequired()
					.HasColumnName("info")
					.HasMaxLength(500);

				entity.HasOne(d => d.IdOrderListNavigation)
					.WithMany(p => p.OrderTable)
					.HasForeignKey(d => d.IdOrderList)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("order_table_id_order_list_fkey");
			});

			modelBuilder.Entity<OrderToService>(entity =>
			{
				entity.ToTable("order_to_service");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.IdOrder).HasColumnName("id_order");

				entity.Property(e => e.IdService).HasColumnName("id_service");

				entity.HasOne(d => d.IdOrderNavigation)
					.WithMany(p => p.OrderToService)
					.HasForeignKey(d => d.IdOrder)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("order_to_service_id_order_fkey");

				entity.HasOne(d => d.IdServiceNavigation)
					.WithMany(p => p.OrderToService)
					.HasForeignKey(d => d.IdService)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("order_to_service_id_service_fkey");
			});

			modelBuilder.Entity<Orderslist>(entity =>
			{
				entity.ToTable("orderslist");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.IdWorker).HasColumnName("id_worker");

				entity.HasOne(d => d.IdWorkerNavigation)
					.WithMany(p => p.Orderslist)
					.HasForeignKey(d => d.IdWorker)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("orderslist_id_worker_fkey");
			});

			modelBuilder.Entity<Photo>(entity =>
			{
				entity.ToTable("photo");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.IdUser).HasColumnName("id_user");

				entity.Property(e => e.Link)
					.IsRequired()
					.HasColumnName("link")
					.HasMaxLength(256);

				entity.HasOne(d => d.IdUserNavigation)
					.WithMany(p => p.Photo)
					.HasForeignKey(d => d.IdUser)
					.HasConstraintName("photo_id_user_fkey");
			});

			modelBuilder.Entity<ProfCategory>(entity =>
			{
				entity.ToTable("prof_category");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasMaxLength(45);
			});

			modelBuilder.Entity<Profession>(entity =>
			{
				entity.ToTable("profession");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.IdProfCategory).HasColumnName("id_prof_category");

				entity.Property(e => e.IdWorker).HasColumnName("id_worker");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasMaxLength(45);

				entity.HasOne(d => d.IdProfCategoryNavigation)
					.WithMany(p => p.Profession)
					.HasForeignKey(d => d.IdProfCategory)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("profession_id_prof_category_fkey");

				entity.HasOne(d => d.IdWorkerNavigation)
					.WithMany(p => p.Profession)
					.HasForeignKey(d => d.IdWorker)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("profession_id_worker_fkey");
			});

			modelBuilder.Entity<Service>(entity =>
			{
				entity.ToTable("service");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.IdProffesion).HasColumnName("id_proffesion");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasMaxLength(100);

				entity.Property(e => e.Price)
					.HasColumnName("price")
					.HasColumnType("money");

				entity.HasOne(d => d.IdProffesionNavigation)
					.WithMany(p => p.Service)
					.HasForeignKey(d => d.IdProffesion)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("service_id_proffesion_fkey");
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.ToTable("user_table");

				entity.HasIndex(e => e.IdWorker)
					.HasName("user_id_worker_key")
					.IsUnique();

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasDefaultValueSql("nextval('user_id_seq'::regclass)");

				entity.Property(e => e.IdWorker).HasColumnName("id_worker");

				entity.Property(e => e.Login)
					.IsRequired()
					.HasColumnName("login")
					.HasMaxLength(256);

				entity.Property(e => e.Password)
					.IsRequired()
					.HasColumnName("password")
					.HasMaxLength(256);

				entity.HasOne(d => d.IdWorkerNavigation)
					.WithOne(p => p.User)
					.HasForeignKey<User>(d => d.IdWorker)
					.HasConstraintName("user_id_worker_fkey");
			});

			modelBuilder.Entity<Worker>(entity =>
			{
				entity.ToTable("worker");

				entity.HasIndex(e => e.IdAddress)
					.HasName("workertoaddress");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.IdAddress).HasColumnName("id_address");

				entity.Property(e => e.Info)
					.IsRequired()
					.HasColumnName("info")
					.HasMaxLength(500);

				entity.Property(e => e.LastName)
					.IsRequired()
					.HasColumnName("last_name")
					.HasColumnType("character(30)");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasColumnType("character(30)");

				entity.Property(e => e.PhoneNumber)
					.IsRequired()
					.HasColumnName("phone_number")
					.HasMaxLength(15);

				entity.HasOne(d => d.IdAddressNavigation)
					.WithMany(p => p.Worker)
					.HasForeignKey(d => d.IdAddress)
					.HasConstraintName("worker_address_fkey");
			});

			modelBuilder.HasSequence<int>("users_id_seq");
		}
	}
}
