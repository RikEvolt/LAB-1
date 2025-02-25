using Microsoft.EntityFrameworkCore;
using PRN222.ProductStore.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.ProductStore.Repository.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private DbContext dbContext;
		private DbSet<T> dbset;
		public GenericRepository(ProductStoreContext productStoreContext)
		{
			this.dbContext = productStoreContext;
			this.dbset = productStoreContext.Set<T>();
		}

		public void Add(T entity)
		{
			dbset.Add(entity);
		}

		public async Task AddAsync(T entity)
		{
			dbset.AddAsync(entity);
		}

		public void Delete(T entity)
		{
			dbset.Remove(entity);
		}

		public async Task DeleteAsync(T entity)
		{
			dbset.Remove(entity);
		}

		public void DeleteById(int id)
		{
			var e = dbset.Find(id);
			if (e != null) dbset.Remove(e);
			else throw new KeyNotFoundException($"{id} was not found.");
		}

		public async Task DeleteByIdAsync(int id)
		{
			var e = await dbset.FindAsync(id);
			if (e != null) dbset.Remove(e);
			else throw new KeyNotFoundException($"{id} was not found.");
		}

		public void DeleteRange(List<T> entities)
		{
			if (entities.Count() > 0) dbset.RemoveRange(entities);
		}

		public async Task DeleteRangeAsync(List<T> entities)
		{
			if (entities.Count() > 0) dbset.RemoveRange(entities);
		}

		public IEnumerable<T> GetAll()
		{
			if (dbset != null) return dbset;
			else throw new KeyNotFoundException($"Data was not found.");
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			if (dbset != null) return dbset;
			else throw new KeyNotFoundException($"Data was not found.");
		}

		public T? GetById(int id)
		{
			var data = dbset.Find(id);
			if (data != null) return data;
			else throw new KeyNotFoundException($"{id} was not found.");
		}

		public async Task<T> GetByIdAsync(int id)
		{
			var data = await dbset.FindAsync(id);
			if (data != null) return data;
			else throw new KeyNotFoundException($"{id} was not found.");
		}

		public void Update(T entity)
		{
			dbset.Update(entity);
		}

		public async Task UpdateAsync(T entity)
		{
			dbset.Update(entity);
		}
	}

}
