using Microsoft.EntityFrameworkCore;
using PRN222.ProductStore.Repository.Entities;
using System;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Repository.Repositories
{
	public class AccountRepository : GenericRepository<AccountMember>, IAccountRepository
	{
		private readonly ProductStoreContext _context;
		private readonly DbSet<AccountMember> _dbSet;

		public AccountRepository(ProductStoreContext context) : base(context)
		{
			_dbSet = context.Set<AccountMember>();
		}

		public async Task<AccountMember?> GetAccountByEmailAsync(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				return null;
			}

			return await _dbSet.FirstOrDefaultAsync(x => x.EmailAddress == email);
		}
	}
}
