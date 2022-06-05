using CloudMining.DataContext;
using CloudMining.Models;
using CloudMining.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudMining.Repositories
{
	public class PurchasesRepository : BaseRepository<Purchase>
	{
		public PurchasesRepository(BaseDataContext dbContext) : base(dbContext) { }

		public override IQueryable<Purchase> GetAll()
		{
			return base.GetAll().Include(p => p.Shares);
		}

		public override Purchase Create(Purchase newPurchase)
		{
			Purchase resultPurchase = base.Create(newPurchase);
			
			if (resultPurchase.IsMandatory == true)
				SplitPurchase(resultPurchase);

			return base.GetById(resultPurchase.Id);
		}

		private void SplitPurchase(Purchase newPurchase)
		{
			List<Member> Members = new MembersRepository(new BaseDataContext()).GetAll().ToList();
			IRepository<PurchaseShare> PurchaseSharesRepository = new PurchaseSharesRepository(new BaseDataContext());

			foreach (var member in Members)
			{
				PurchaseSharesRepository.Create(
				new PurchaseShare
				{
					BaseEntity = newPurchase,
					Member = member,
					Percent = member.Share,
					Amount = Math.Round(newPurchase.Amount * member.Share / 100, 0, MidpointRounding.AwayFromZero),
					IsDone = false
				});
			}
		}
	}
}
