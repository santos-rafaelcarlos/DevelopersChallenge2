using System;
using System.IO;
using System.Xml;
using Xayah.Model;
using Xayah.Model.Interfaces;
using Xayah.Impl.Extension;
using System.Text.RegularExpressions;

namespace Xayah.Impl
{
	public class OfxReader : IOfxReader
	{
		private const string transferPatern = @"<STMTTRN>[\s\S]*?(?=\n.*?<\/STMTTRN>|$)";
		public OfxFile ReadFile(string fileName, Stream file)
		{
			var retval = new OfxFile();
			string fileTxt = string.Empty;

			using (var read = new StreamReader(file))
				fileTxt = read.ReadToEnd();
		
			foreach (Match m in Regex.Matches(fileTxt, transferPatern))
			{
				var node = m.Value;

				retval.Transfers.Add(new BankTransfer
				{
					Date = node.GetNodeValue("DTPOSTED").ToOfxDate(),
					Description = node.GetNodeValue("MEMO"),
					Type = node.GetNodeValue("TRNTYPE").ToTransferType(),
					Value = node.GetNodeValue("TRNAMT").ToAmount(),
					FileName = fileName,
				});
			}

			return retval;
		}
	}
}
