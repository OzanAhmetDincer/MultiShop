using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Entities;
using System.Data;

namespace MultiShop.Discount.Context
{
    public class DapperContext : DbContext
    {
        private readonly IConfiguration _configuration; //Bu, konfigürasyon ayarlarını okumak için kullanılan bir IConfiguration nesnesidir. Genellikle appsettings.json dosyasından veya diğer konfigürasyon kaynaklarından ayarları almak için kullanılır.
        private readonly string _connectionString;//Bu, veritabanına bağlanmak için kullanılan bağlantı dizesidir. IConfiguration nesnesinden alınır ve DefaultConnection anahtarıyla yapılandırılır.

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;// Gelen konfigürasyon nesnesini sınıfın _configuration alanına atar.
            _connectionString = _configuration.GetConnectionString("DefaultConnection");//"DefaultConnection" adıyla yapılandırılmış bağlantı dizesini alır ve _connectionString alanına atar.
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; database=MultiShopDiscountDb; integrated security=true;");
        }
        public DbSet<Coupon> Coupons { get; set; }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);// Bu metod, "_connectionString" kullanarak yeni bir "SqlConnection" nesnesi oluşturur ve döner. Bu, Dapper ile veritabanı bağlantıları oluşturmak için kullanılır. "IDbConnection" arayüzünü döndürür, böylece bağlantının soyutlanmasını sağlar ve diğer veritabanı bağlantı türleriyle de kullanılabilir hale gelir.

        /*  "DapperContext" sınıfı, hem EF Core hem de Dapper ile kullanılmak üzere tasarlanmış bir veritabanı bağlamıdır.
            "IConfiguration" üzerinden alınan bağlantı dizesi ile veritabanına bağlanır.
            "OnConfiguring" metodu, EF Core'un SQL Server'ı kullanmasını sağlar.
            "Coupons" DbSet özelliği, "Coupon" nesnelerinin veritabanı ile etkileşimde bulunmasını sağlar.
            "CreateConnection" metodu, Dapper kullanarak veritabanı bağlantıları oluşturur.*/
    }
}
