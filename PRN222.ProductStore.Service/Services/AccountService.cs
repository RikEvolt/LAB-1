using AutoMapper;
using PRN222.ProductStore.Repository.Repositories;
using PRN222.ProductStore.Service.BusinessModels;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Service.Services
{
	public class AccountService : IAccountService
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IMapper _mapper;

		public AccountService(IAccountRepository accountRepository, IMapper mapper)
		{
			_accountRepository = accountRepository;
			_mapper = mapper;
		}

		public async Task<AccountMemberDTO?> GetAccountByEmailAsync(string email)
		{
			var account = await _accountRepository.GetAccountByEmailAsync(email);
			return account != null ? _mapper.Map<AccountMemberDTO>(account) : null;
		}
	}
}
