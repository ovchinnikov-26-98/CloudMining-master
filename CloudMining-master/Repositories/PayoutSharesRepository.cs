using CloudMining.DataContext;
using CloudMining.Models;
using CloudMining.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CloudMining.Repositories
{
	public class PayoutSharesRepository : BaseRepository<PayoutShare>
	{
		public PayoutSharesRepository(BaseDataContext dbContext) : base(dbContext) { }

		public override IQueryable<PayoutShare> GetAll()
		{
			return base.GetAll().Include(item => item.Member).Include(item => item.BaseEntity.Currency);
		}
	}
}
