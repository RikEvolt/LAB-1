using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PRN222.ProductStore.Repository.Entities;
using PRN222.ProductStore.Repository.Repositories;
using PRN222.ProductStore.Service.BusinessModels;

namespace PRN222.ProductStore.Service.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<List<ProductDTO>> GetProductsAsync()
		{
			var products = await _unitOfWork.ProductRepository.GetProducts();
			return products.Select(p => new ProductDTO
			{
				ProductId = p.ProductId,
				ProductName = p.ProductName,
				CategoryId = p.CategoryId,
				CategoryName = p.Category.CategoryName,
				UnitslnStock = p.UnitslnStock,
				UnitPrice = p.UnitPrice
			}).ToList();
		}


		public async Task<ProductDTO> GetProductByIdAsync(int id)
		{
			var product = await _unitOfWork.ProductRepository.GetProductById(id);
			return _mapper.Map<ProductDTO>(product);
		}

		public async Task CreateProductAsync(ProductDTO productDTO)
		{
			var product = _mapper.Map<Product>(productDTO);
			await _unitOfWork.ProductRepository.AddAsync(product);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task UpdateProductAsync(ProductDTO productDTO)
		{
			var product = _mapper.Map<Product>(productDTO);
			await _unitOfWork.ProductRepository.UpdateAsync(product);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task DeleteProductAsync(int id)
		{
			var product = await _unitOfWork.ProductRepository.GetProductById(id);
			if (product != null)
			{
				await _unitOfWork.ProductRepository.DeleteAsync(product);
				await _unitOfWork.SaveChangesAsync();
			}
		}
	}
}
