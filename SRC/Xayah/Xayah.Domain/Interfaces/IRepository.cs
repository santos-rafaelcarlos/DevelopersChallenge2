using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Xayah.Model.Interfaces
{
	public interface IRepository<TEntity> where TEntity : class
    {
		void Add(params TEntity[] items);
		void Remove(params TEntity[] items);
		void Remove(long id);
		IEnumerable<TEntity> GetAll();
		IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
		
    }
}
