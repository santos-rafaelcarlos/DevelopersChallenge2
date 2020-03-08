using System.Collections.Generic;
using Xayah.Model;
using Xayah.Model.Interfaces;

namespace Xayah.Impl
{
	public class ConciliateService : IConciliateService
	{
		private readonly ICheckTransfer _checkTransfer;
		public ConciliateService(ICheckTransfer checkTransfer)
		{
			_checkTransfer = checkTransfer;
		}


		public IEnumerable<BankTransfer> Reconcile(params OfxFile[] files)
		{
			var retval = new List<BankTransfer>();

			foreach (var f in files)
				foreach (var item in f.Transfers)
				{
					if (!_checkTransfer.Exist(item, retval, true))
						retval.Add(item);
				}

			return retval;
		}
	}
}
