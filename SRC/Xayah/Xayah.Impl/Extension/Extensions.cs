using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using Xayah.Model;

namespace Xayah.Impl.Extension
{
	public static class Extensions
	{
		public static DateTime ToOfxDate(this String str)
		{
			try
			{
				string format = "yyyyMMddHHmmss";
				var input = Regex.Replace(str, @"\[[\s\S]*?\]", string.Empty).Replace("\r",string.Empty);
				
				return DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);
			}
			catch
			{
				throw new InvalidCastException();
			}
		}


		public static string GetNodeValue(this string node, string nodeName)
		{
			return Regex.Match(node, $"<{nodeName}>.*").Value.Replace($"<{nodeName}>", string.Empty);
		}

		public static TransferType ToTransferType(this string node)
		{
			return string.Compare(node.Replace("\r",string.Empty), "CREDIT", true) == 0 ?
				TransferType.CREDIT : TransferType.DEBIT;
		}

		public static double ToAmount(this string node)
		{
			var retval = double.MinValue;

			if (!double.TryParse(node,NumberStyles.Any,CultureInfo.GetCultureInfo("en-US"), out retval))
				throw new InvalidCastException();
			return retval;
		}

	}
}
