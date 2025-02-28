using System;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Repository.Repositories
{
	public interface IUnitOfWork
	{
		IProductRepository ProductRepository { get; }
		ICategoryRepository CategoryRepository { get; }
		IAccountRepository AccountRepository { get; }

		Task<int> SaveChangesAsync();
	}
}
