using Microsoft.EntityFrameworkCore;
using PRN222.ProductStore.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Repository.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ProductStoreContext _context;

		public CategoryRepository(ProductStoreContext context)
		{
			_context = context;
		}

		public async Task<List<Category>> GetCategoriesAsync()
		{
			return await _context.Categories.ToListAsync();
		}

	}
}
