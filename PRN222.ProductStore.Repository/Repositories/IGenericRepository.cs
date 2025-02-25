using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Repository.Repositories
{
	public interface IGenericRepository<T> where T : class
	{
		T GetById(int id);
		Task<T> GetByIdAsync(int id);
		IEnumerable<T> GetAll();
		Task<IEnumerable<T>> GetAllAsync();
		void Add(T entity);
		Task AddAsync(T entity);
		void Update(T entity);
		Task UpdateAsync(T entity);
		void DeleteById(int id);
		Task DeleteByIdAsync(int id);
		void Delete(T entity);
		Task DeleteAsync(T entity);
		void DeleteRange(List<T> entities);
		Task DeleteRangeAsync(List<T> entities);
	}

}
