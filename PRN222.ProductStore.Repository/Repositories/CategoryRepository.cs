using Microsoft.EntityFrameworkCore;
using PRN222.ProductStore.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Repository.Repositories
{
	public class CategoryRepository : GenericRepository<Category> ,ICategoryRepository
	{
		private readonly ProductStoreContext _context;
		private readonly DbSet<Category> _dbSet;

		public CategoryRepository(ProductStoreContext context) : base(context)
		{
			_dbSet = context.Set<Category>();
		}

		public async Task<List<Category>> GetCategoriesAsync()
		{
			return await _dbSet.ToListAsync();
		}

	}
}
