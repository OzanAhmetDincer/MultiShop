using Dapper;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _dapperContext;

        public DiscountService(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto createCouponDto)
        {
            //query: Bu değişken, SQL sorgusunu içerir. Bu sorgu, Coupons tablosuna yeni bir kayıt eklemek için kullanılır. "insert into Coupons (Code,Rate,IsActive,ValidDate) values (@code,@rate,@isActive,@validDate)": Bu SQL ifadesi, Coupons tablosuna yeni bir kupon eklemek için gerekli olan değerleri belirtir. Sorgu, parametre yer tutucuları (@code, @rate, @isActive, @validDate) kullanır.
            string query = "insert into Coupons (Code,Rate,IsActive,ValidDate) values (@code,@rate,@isActive,@validDate)";//(@code,@rate,@isActive,@validDate): Bu kısım, yukarıda belirtilen sütunlara karşılık gelen değerlerin yer tutucularıdır. Yer tutucular, sorguya parametre olarak geçilecek değerleri temsil eder. Bu değerler, kodda Dapper ile dinamik olarak sağlanır.

            var parameters = new DynamicParameters();//DynamicParameters: Bu sınıf, Dapper ile birlikte kullanılan dinamik parametrelerin tutulduğu bir sınıftır.
            parameters.Add("@code", createCouponDto.Code);// "parameters.Add": Bu metodlar, SQL sorgusundaki parametre yer tutucularına karşılık gelen değerleri createCouponDto nesnesinden alarak ekler.
            parameters.Add("@rate", createCouponDto.Rate);
            parameters.Add("@isActive", createCouponDto.IsActive);
            parameters.Add("@validDate", createCouponDto.ValidDate);

            /*using: Bu deyim, connection nesnesinin kapsamı bittiğinde otomatik olarak Dispose edilmesini sağlar, yani bağlantı kapatılır ve kaynaklar serbest bırakılır.
            _dapperContext.CreateConnection(): Bu metod, DapperContext sınıfındaki CreateConnection metodunu çağırarak yeni bir veritabanı bağlantısı oluşturur.
            await connection.ExecuteAsync(query, parameters): Bu satır, Dapper'ın ExecuteAsync metodunu kullanarak SQL sorgusunu parametrelerle birlikte asenkron olarak yürütür. Sorgu, Coupons tablosuna yeni bir kupon ekler.*/
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteDiscountCouponAsync(int id)
        {
            string query = "Delete From Coupons where CouponId=@couponId";
            var parameters = new DynamicParameters();
            parameters.Add("couponId", id);
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync()
        {
            string query = "Select * From Coupons";
            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultDiscountCouponDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int id)
        {
            string query = "Select * From Coupons Where CouponId=@couponId";
            var parameters = new DynamicParameters();
            parameters.Add("@couponId", id);
            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query, parameters);
                return values;
            }
        }

        public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateCouponDto)
        {
            string query = "Update Coupons Set Code=@code,Rate=@rate,IsActive=@isActive,ValidDate=@validDate where CouponId=@couponId";
            var parameters = new DynamicParameters();
            parameters.Add("@code", updateCouponDto.Code);
            parameters.Add("@rate", updateCouponDto.Rate);
            parameters.Add("@isActive", updateCouponDto.IsActive);
            parameters.Add("@validDate", updateCouponDto.ValidDate);
            parameters.Add("@couponId", updateCouponDto.CouponId);
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
