using CloudMining.DataContext;
using CloudMining.Models;
using CloudMining.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CloudMining.Repositories
{
	public class DepositsRepository : BaseRepository<Deposit>
	{
		public DepositsRepository(BaseDataContext dbContext) : base(dbContext) { }

		public override IQueryable<Deposit> GetAll()
		{
			return base.GetAll().Include(item => item.Member);
		}
	}
}
