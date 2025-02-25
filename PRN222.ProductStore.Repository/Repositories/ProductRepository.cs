using Microsoft.EntityFrameworkCore;
using PRN222.ProductStore.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Repository.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		private readonly ProductStoreContext _context;
		private readonly DbSet<Product> _dbSet;

		public ProductRepository(ProductStoreContext context) : base(context)
		{
			_dbSet = context.Set<Product>();
		}

		public async Task<List<Product>> GetProductsAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<Product?> GetProductByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task SaveProductAsync(Product p)
		{
			await _dbSet.AddAsync(p);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateProductAsync(Product p)
		{
			_dbSet.Update(p);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteProductAsync(Product p)
		{
			var product = await _dbSet.FindAsync(p.ProductId);
			if (product != null)
			{
				_dbSet.Remove(product);
				await _context.SaveChangesAsync();
			}
		}
	}
}
