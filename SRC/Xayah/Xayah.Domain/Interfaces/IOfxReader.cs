using System.IO;

namespace Xayah.Model.Interfaces
{
	public interface IOfxReader
    {
		OfxFile ReadFile(string fileName,Stream file);
    }
}
