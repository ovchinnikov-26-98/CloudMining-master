using CloudMining.DataContext;
using CloudMining.Models;
using CloudMining.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CloudMining.Repositories
{
	public class PurchaseSharesRepository : BaseRepository<PurchaseShare>
	{
		public PurchaseSharesRepository(BaseDataContext dbContext) : base(dbContext) { }

		public override IQueryable<PurchaseShare> GetAll()
		{
			return base.GetAll().Include(p => p.Member).Include(p => p.BaseEntity);
		}
	}
}
