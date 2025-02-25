using AutoMapper;
using PRN222.ProductStore.Repository.Entities;
using PRN222.ProductStore.Repository.Repositories;
using PRN222.ProductStore.Service.BusinessModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Service.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;

		public ProductService(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public async Task<List<ProductDTO>> GetProductsAsync()
		{
			var products = await _productRepository.GetProductsAsync();
			return _mapper.Map<List<ProductDTO>>(products);
		}

		public async Task<ProductDTO> GetProductByIdAsync(int id)
		{
			var product = await _productRepository.GetProductByIdAsync(id);
			return _mapper.Map<ProductDTO>(product);
		}

		public async Task CreateProductAsync(ProductDTO productDTO)
		{
			var product = _mapper.Map<Product>(productDTO);
			await _productRepository.SaveProductAsync(product);
		}

		public async Task UpdateProductAsync(ProductDTO productDTO)
		{
			var product = _mapper.Map<Product>(productDTO);
			await _productRepository.UpdateProductAsync(product);
		}

		public async Task DeleteProductAsync(ProductDTO productDTO)
		{
			var product = _mapper.Map<Product>(productDTO);
			await _productRepository.DeleteProductAsync(product);
		}
	}
}
