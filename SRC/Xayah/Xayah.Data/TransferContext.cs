using Microsoft.EntityFrameworkCore;
using Xayah.Model;

namespace Xayah.Data
{
	public interface ITransferContext
	{
		DbSet<BankTransfer> BankTransfer { get; set; }
		void SaveChanges();
	}

	public class TransferContext : DbContext, ITransferContext
    {
		public TransferContext(DbContextOptions<TransferContext> options)
			:base(options)
		{
			
		}

		public DbSet<BankTransfer> BankTransfer { get; set; }

		void ITransferContext.SaveChanges()
		{
			this.SaveChanges();
		}
	}
}
