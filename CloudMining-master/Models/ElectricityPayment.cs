using CloudMining.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CloudMining.Models
{
	public class ElectricityPayment : Entity
	{
		public double Amount { get; set; }
		public DateTime Date { get; set; }
		public List<ElectricityPaymentShare> Shares { get; set; }

		[NotMapped]
		public bool IsDone => Shares.Count(s => s.IsDone == true) == Shares.Count;
	}
}
