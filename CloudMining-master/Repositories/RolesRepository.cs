using CloudMining.DataContext;
using CloudMining.Models;
using CloudMining.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CloudMining.Repositories
{
	public class RolesRepository : BaseRepository<Role>
	{
		public RolesRepository(BaseDataContext dbContext) : base(dbContext) { }

		public override IQueryable<Role> GetAll()
		{
			return base.GetAll().Include(item => item.Members);
		}
	}
}
