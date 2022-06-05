using CloudMining.Models.Base;
using System.Collections.Generic;

namespace CloudMining.Models
{
	public class Role : NamedEntity
	{
		public double Fee { get; set; }
		public List<Member> Members { get; set; }
	}
}
