using System.Collections.Generic;

namespace Xayah.Model.Interfaces
{
	public interface ICheckTransfer
	{
		bool Exist(BankTransfer item);
		bool Exist(BankTransfer item, IEnumerable<BankTransfer> source, bool checkFile  = false);
	}
}
