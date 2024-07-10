using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        // Constructor içerisinde mongodb veri tabanı bağlantımıza ait yapılandırmaları yaparız. Constructor da direkt appsettings e erişemeyiz. Bu bağlantıya ait bilgiler appsettings içerisinde. Appsetting ile bağlantıyı sağlayacak olan "IDatabaseSettings" interface'ini kullanan "DatabaseSettings" ile veri tabanı bağlantımızı sağlarız. İlk önce "ConnectionString" ile veri tabanı bağlantımızı belirtiriz. Sonrasında "client" üzerinden veri tabanımıza geçeriz(DatabaseName). Sonrasında veri tabanında hangi koleksiyon doldurulacaksa onu belirtiriz. "Category" türünde "CategoryCollectionName" ismindeki yer.
        public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var value = _mapper.Map<Category>(createCategoryDto);
            await _categoryCollection.InsertOneAsync(value);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteManyAsync(c => c.CategoryId == id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync()
        {
            var values = await _categoryCollection.Find(c => true).ToListAsync();// "c=>true" ile tüm değerler gelir.
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        public async Task<GetByIdCategoryDto> GetCategoryByIdAsync(string id)
        {
            var values = await _categoryCollection.Find<Category>(c => c.CategoryId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdCategoryDto>(values);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var values = _mapper.Map<Category>(updateCategoryDto);
            await _categoryCollection.FindOneAndReplaceAsync(c => c.CategoryId == updateCategoryDto.CategoryId, values);
        }
    }
}
