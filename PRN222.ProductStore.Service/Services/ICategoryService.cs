using PRN222.ProductStore.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Service.Services
{
	public interface ICategoryService
	{
		Task<List<CategoryDTO>> GetCategories();
	}
}
