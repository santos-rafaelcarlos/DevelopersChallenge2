using System.Collections.Generic;

namespace Xayah.Model.Interfaces
{
	public interface IConciliateService
    {
		IEnumerable<BankTransfer> Reconcile(params OfxFile[] files);
    }
}
