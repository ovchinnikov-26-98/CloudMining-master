using CloudMining.DataContext;
using CloudMining.Models;
using CloudMining.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CloudMining.Repositories
{
	public class MembersRepository : BaseRepository<Member>
	{
		public MembersRepository(BaseDataContext dbContext) : base(dbContext) { }

		public override IQueryable<Member> GetAll()
		{
			return base.GetAll().Include(item => item.Role).Include(item => item.Deposits);
		}
	}
}
