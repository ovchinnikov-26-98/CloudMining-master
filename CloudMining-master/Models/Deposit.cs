using CloudMining.Models.Base;
using System;

namespace CloudMining.Models
{
	public class Deposit : Entity
	{
		public Member Member { get; set; }
		public DateTime Date { get; set; }
		public double Amount { get; set; }
		public string Comment { get; set; }
    }
}
