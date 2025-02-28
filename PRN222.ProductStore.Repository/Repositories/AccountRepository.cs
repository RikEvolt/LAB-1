using Microsoft.EntityFrameworkCore;
using PRN222.ProductStore.Repository.Entities;
using System;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Repository.Repositories
{
	public class AccountRepository : GenericRepository<AccountMember>, IAccountRepository
	{
		private readonly ProductStoreContext _context;

		public AccountRepository(ProductStoreContext context) : base(context)
		{
			_context = context;
		}

		public async Task<AccountMember?> GetAccountByEmailAsync(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
			{
				return null;
			}

			return await _context.AccountMembers.FirstOrDefaultAsync(x => x.EmailAddress == email);
		}
	}
}
