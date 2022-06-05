using CloudMining.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CloudMining.Models
{
	public class Purchase : Entity
	{
		public DateTime Date { get; set; }
		public double Amount { get; set; }
		public string Subject { get; set; }
		public bool IsMandatory { get; set; }
		public List<PurchaseShare> Shares { get; set; }

		[NotMapped]
		public bool IsDone => Shares.Count(s => s.IsDone == true) == Shares.Count;
	}
}
