using System;
using System.Collections.Generic;

namespace Xayah.Model
{
	public class OfxFile
	{
		public OfxFile()
		{
			this.Transfers = new List<BankTransfer>();
		}
				
		public IList<BankTransfer> Transfers { get; set; }
	}
}
