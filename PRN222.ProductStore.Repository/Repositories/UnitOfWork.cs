using System;
using System.Threading.Tasks;
using PRN222.ProductStore.Repository.Entities;

namespace PRN222.ProductStore.Repository.Repositories
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly ProductStoreContext _context;
		public IProductRepository ProductRepository { get; }
		public ICategoryRepository CategoryRepository { get; }
		public IAccountRepository AccountRepository { get; }

		public UnitOfWork(
			ProductStoreContext context,
			IProductRepository productRepository,
			ICategoryRepository categoryRepository,
			IAccountRepository accountRepository)
		{
			_context = context;
			ProductRepository = productRepository;
			CategoryRepository = categoryRepository;
			AccountRepository = accountRepository;
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
