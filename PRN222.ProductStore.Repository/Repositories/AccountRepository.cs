using Microsoft.EntityFrameworkCore;
using PRN222.ProductStore.Repository.Entities;
using System;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Repository.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly ProductStoreContext _context;

		public AccountRepository(ProductStoreContext context)
		{
			_context = context;
		}

		public async Task<AccountMember?> GetAccountByIdAsync(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				return null;
			}

			return await _context.AccountMembers.FirstOrDefaultAsync(x => x.EmailAddress == email);
		}
	}
}
