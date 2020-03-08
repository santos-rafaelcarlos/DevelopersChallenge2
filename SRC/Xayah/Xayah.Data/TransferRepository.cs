using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xayah.Model;
using Xayah.Model.Interfaces;

namespace Xayah.Data
{
	public class TransferRepository : IRepository<BankTransfer>, ICheckTransfer
	{
		private readonly ITransferContext _context;
		public TransferRepository(ITransferContext context)
		{
			_context = context;
		}

		public void Add(params BankTransfer[] items)
		{
			foreach (var item in items)
			{
				item.LastUpdate = DateTime.Now;
				if (_context.BankTransfer.Any(x => x.Id == item.Id))
					_context.BankTransfer.Update(item);
				else
					_context.BankTransfer.Add(item);
			}
			_context.SaveChanges();
		}

		public bool Exist(BankTransfer item)
		{
			return Exist(item, _context.BankTransfer);				
		}

		public bool Exist(BankTransfer item, IEnumerable<BankTransfer> source, bool checkFile = false)
		{
			return source.Any(x => x.Type == item.Type
								&& x.Value == item.Value
								&& x.Date == item.Date
								&& x.Description.ToLower() == item.Description.ToLower()
								&& (!checkFile || x.FileName != item.FileName));
		}

		public IEnumerable<BankTransfer> Get(Expression<Func<BankTransfer, bool>> predicate)
		{
			return _context.BankTransfer.Where(predicate);
		}

		public IEnumerable<BankTransfer> GetAll()
		{
			return _context.BankTransfer.OrderByDescending(x=> x.Date).AsEnumerable();
		}

		public void Remove(params BankTransfer[] items)
		{
			_context.BankTransfer.RemoveRange(items);
			_context.SaveChanges();
		}

		public void Remove(long id)
		{
			var item = Get(x => x.Id == id).FirstOrDefault();
			Remove(item);
		}
	}
}
