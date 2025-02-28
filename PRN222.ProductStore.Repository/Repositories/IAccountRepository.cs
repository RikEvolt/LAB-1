﻿using PRN222.ProductStore.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Repository.Repositories
{
	public interface IAccountRepository : IGenericRepository<AccountMember>
	{
		Task<AccountMember?> GetAccountByEmailAsync(string email, string password);
	}
}
