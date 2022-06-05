using CloudMining.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CloudMining.Models
{
	public class Payout : Entity
	{
		public string TxId { get; set; }
		public double Timestamp { get; set; }
		public double Amount { get; set; }
		public Currency Currency { get; set; }
		public List<PayoutShare> Shares { get; set; }

		[NotMapped]
		public DateTime Date => new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(this.Timestamp).ToLocalTime();
		public bool IsDone => Shares.Count(s => s.IsDone == true) == Shares.Count;
	}
}