using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xayah.Model
{
    public class BankTransfer
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public TransferType Type { get; set; }
		public DateTime Date { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
		public double Value { get; set; }
		public string Description { get; set; }		
		public DateTime LastUpdate{ get; set; }		
		public string FileName { get; set; }
	}
}
