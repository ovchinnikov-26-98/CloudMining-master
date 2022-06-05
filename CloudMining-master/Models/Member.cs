using CloudMining.DataContext;
using CloudMining.Models.Base;
using CloudMining.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CloudMining.Models
{
	public class Member : NamedEntity
	{
		public Role Role { get; set; }
		public DateTime JoinDate { get; set; }
		public List<Deposit> Deposits { get; set; }

		[NotMapped]
		public double DepositsAmount => Deposits.Sum(d => d.Amount);
		public double Share => Math.Round(DepositsAmount / new DepositsRepository(new BaseDataContext()).GetAll().Sum(d => d.Amount) * 100, 2); 
	}
}
