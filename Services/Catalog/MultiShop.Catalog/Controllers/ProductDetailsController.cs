using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Services.ProductDetailServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailsController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetailList()
        {
            var results = await _productDetailService.GetAllProductDetailsAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailById(string id)
        {
            var result = await _productDetailService.GetProductDetailByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetailProduct(CreateProductDetailDto createProductDetailDto)
        {
            await _productDetailService.CreateProductDetailAsync(createProductDetailDto);
            return Ok("Ürün Detayı Başarıyla Eklendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductDetail(string id)
        {
            await _productDetailService.DeleteProductDetailAsync(id);
            return Ok("Ürün Detayı Başarıyla Silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDetailDto updateProductDetailDto)
        {
            await _productDetailService.UpdateProductDetailAsync(updateProductDetailDto);
            return Ok("Ürün Detayı Başarıyla Güncellendi.");
        }
    }
}
