using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMining.Models.Base
{
	public abstract class Share<T> : Entity
	{
		private T _BaseEntity;
		public T BaseEntity
		{
			get => _BaseEntity;
			set => Set(ref _BaseEntity, value);
		}

		private Member _Member;
		public Member Member
		{
			get => _Member;
			set => Set(ref _Member, value);
		}

		private double _Percent;
		public double Percent
		{
			get => _Percent;
			set => Set(ref _Percent, value);
		}

		private double _Amount;
		public double Amount
		{
			get => _Amount;
			set => Set(ref _Amount, value);
		}

		private bool _IsDone;
		public bool IsDone
		{
			get => _IsDone;
			set => Set(ref _IsDone, value);
		}
	}
}
