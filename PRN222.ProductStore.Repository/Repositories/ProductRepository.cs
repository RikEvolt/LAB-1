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

		public ProductRepository(ProductStoreContext context) : base(context)
		{
			_context = context;
		}

		public async Task<Product> GetProductById(int id)
		{
			return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
		}

		public async Task<List<Product>> GetProducts()
		{
			return await _context.Products.Include(p => p.Category).ToListAsync();
		}
	}
}
