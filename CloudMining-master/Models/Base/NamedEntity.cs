using System.ComponentModel.DataAnnotations;

namespace CloudMining.Models.Base
{
	public abstract class NamedEntity : Entity
	{
		[Required]
		public string Name { get; set; }
	}
}
