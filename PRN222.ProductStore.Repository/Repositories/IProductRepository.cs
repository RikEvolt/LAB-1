using PRN222.ProductStore.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Repository.Repositories
{
	public interface IProductRepository
	{
		Task<List<Product>> GetProductsAsync();
		Task SaveProductAsync(Product p);
		Task UpdateProductAsync(Product p);
		Task DeleteProductAsync(Product p);
		Task<Product> GetProductByIdAsync(int id);
	}
}
