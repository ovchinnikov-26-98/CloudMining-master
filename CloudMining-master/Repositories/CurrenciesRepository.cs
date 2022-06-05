using CloudMining.DataContext;
using CloudMining.Models;
using CloudMining.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CloudMining.Repositories
{
	public class CurrenciesRepository : BaseRepository<Currency>
	{
		public override IQueryable<Currency> GetAll()
		{
			return base.GetAll();
		}

		public CurrenciesRepository(BaseDataContext dbContext) : base(dbContext) { }
	}
}
