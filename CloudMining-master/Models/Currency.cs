using CloudMining.Models.Base;
using System.Collections.Generic;

namespace CloudMining.Models
{
	public class Currency : NamedEntity
	{
		public string ShortName { get; set; }
		public int Precision { get; set; }
	}
}
