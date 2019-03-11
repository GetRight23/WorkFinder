using Microsoft.EntityFrameworkCore;
using DatabaseConfiguration;

namespace Models
{
	public partial class ApplicationContext : DbContext
	{	
		public string ConnectionString { get; private set; }

		public ApplicationContext(string connectionString)
		{
			ConnectionString = connectionString;
		}

		public virtual DbSet<Address> Address { get; set; }
		public virtual DbSet<City> City { get; set; }
		public virtual DbSet<CityDistricts> CityDistricts { get; set; }
		public virtual DbSet<Feedback> Feedback { get; set; }
		public virtual DbSet<OrderTable> OrderTable { get; set; }
		public virtual DbSet<OrderToService> OrderToService { get; set; }
		public virtual DbSet<Orderslist> Orderslist { get; set; }
		public virtual DbSet<ProfCategory> ProfCategory { get; set; }
		public virtual DbSet<Profession> Profession { get; set; }
		public virtual DbSet<ProfessionToWorker> ProfessionToWorker { get; set; }
		public virtual DbSet<Service> Service { get; set; }
		public virtual DbSet<Worker> Worker { get; set; }
		public virtual DbSet<User> User { get; set; }
		public virtual DbSet<Photo> Photo { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseNpgsql(ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

			modelBuilder.Entity<Address>(entity =>
			{
				entity.ToTable("address");

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
					.HasConstraintName("address_id_city_fkey");

				entity.HasOne(d => d.IdCityDistrictNavigation)
					.WithMany(p => p.Address)
					.HasForeignKey(d => d.IdCityDistrict)
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
					.HasConstraintName("order_to_service_id_order_fkey");

				entity.HasOne(d => d.IdServiceNavigation)
					.WithMany(p => p.OrderToService)
					.HasForeignKey(d => d.IdService)
					.HasConstraintName("order_to_service_id_service_fkey");
			});

			modelBuilder.Entity<Orderslist>(entity =>
			{
				entity.ToTable("orderslist");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.IdWorker).HasColumnName("id_worker");
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

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasMaxLength(45);

				entity.HasOne(d => d.IdProfCategoryNavigation)
					.WithMany(p => p.Profession)
					.HasForeignKey(d => d.IdProfCategory)
					.HasConstraintName("profession_id_prof_category_fkey");
			});

			modelBuilder.Entity<ProfessionToWorker>(entity =>
			{
				entity.ToTable("profession_to_worker");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.IdProfession).HasColumnName("id_profession");

				entity.Property(e => e.IdWorker).HasColumnName("id_worker");

				entity.HasOne(d => d.IdProfessionNavigation)
					.WithMany(p => p.ProfessionToWorker)
					.HasForeignKey(d => d.IdProfession)
					.HasConstraintName("profession_to_worker_id_profession_fkey");

				entity.HasOne(d => d.IdWorkerNavigation)
					.WithMany(p => p.ProfessionToWorker)
					.HasForeignKey(d => d.IdWorker)
					.HasConstraintName("profession_to_worker_id_worker_fkey");
			});

			modelBuilder.Entity<Service>(entity =>
			{
				entity.ToTable("service");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.IdProfession).HasColumnName("id_profession");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasMaxLength(100);

				entity.Property(e => e.Price)
					.HasColumnName("price")
					.HasColumnType("money");
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.ToTable("user_table");

				entity.HasIndex(e => e.IdWorker)
					.HasName("user_table_id_worker_key")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("id");

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
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("user_table_id_worker_fkey");
			});

			modelBuilder.Entity<Worker>(entity =>
			{
				entity.ToTable("worker");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.IdAddress).HasColumnName("id_address");

				entity.Property(e => e.Info)
					.IsRequired()
					.HasColumnName("info")
					.HasMaxLength(500);

				entity.Property(e => e.LastName)
					.IsRequired()
					.HasColumnName("last_name")
					.HasMaxLength(30);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name")
					.HasMaxLength(30);

				entity.Property(e => e.PhoneNumber)
					.IsRequired()
					.HasColumnName("phone_number")
					.HasMaxLength(15);

				entity.HasOne(d => d.IdAddressNavigation)
					.WithMany(p => p.Worker)
					.HasForeignKey(d => d.IdAddress)
					.HasConstraintName("worker_id_address_fkey");
			});
		}
	}
}
