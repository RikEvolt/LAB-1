using PRN222.ProductStore.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Service.Services
{
	public interface IProductService
	{
		Task<List<ProductDTO>> GetProductsAsync();
		Task CreateProductAsync(ProductDTO p);
		Task UpdateProductAsync(ProductDTO p);
		Task DeleteProductAsync(int id);
		Task<ProductDTO> GetProductByIdAsync(int id);
	}
}
