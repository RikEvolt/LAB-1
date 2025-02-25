using Microsoft.EntityFrameworkCore;
using PRN222.ProductStore.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Repository.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ProductStoreContext _context;

		public ProductRepository(ProductStoreContext context)
		{
			_context = context;
		}

		public async Task<List<Product>> GetProductsAsync()
		{
			return await _context.Products.ToListAsync();
		}

		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
		}

		public async Task SaveProductAsync(Product p)
		{
			await _context.Products.AddAsync(p);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateProductAsync(Product p)
		{
			_context.Products.Update(p);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteProductAsync(Product p)
		{
			var product = await _context.Products.FindAsync(p.ProductId);

			if (product != null)
			{
				_context.Products.Remove(product);
				await _context.SaveChangesAsync(); 
			}
		}
	}
}
