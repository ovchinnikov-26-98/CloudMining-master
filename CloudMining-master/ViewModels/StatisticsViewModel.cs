using CloudMining.DataContext;
using CloudMining.Models;
using CloudMining.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Windows;

namespace CloudMining.ViewModels
{
	public class StatisticsViewModel : BaseViewModel
	{
		#region Constructor
		public StatisticsViewModel()
		{
			this.Members = new ObservableCollection<Member>(new MembersRepository(new BaseDataContext()).GetAll().ToList());
			CalculateStatistics();
		}
		#endregion

		#region Properties
		private ObservableCollection<Member> _Members;
		public ObservableCollection<Member> Members
		{
			get => _Members;
			set => Set(ref _Members, value);
		}

		private Member _SelectedMember;
		public Member SelectedMember
		{
			get => _SelectedMember;
			set
			{
				Set(ref _SelectedMember, value);
				CalculateStatistics();
			}
		}

		private double _TotalIncome;
		public double TotalIncome
		{
			get => _TotalIncome;
			set => Set(ref _TotalIncome, value);
		}

		private double _TotalElectricity;
		public double TotalElectricity
		{
			get => _TotalElectricity;
			set => Set(ref _TotalElectricity, value);
		}

		private double _TotalExpenses;
		public double TotalExpenses
		{
			get => _TotalExpenses;
			set => Set(ref _TotalExpenses, value);
		}

		private double _TotalProfit;
		public double TotalProfit
		{
			get => _TotalProfit;
			set => Set(ref _TotalProfit, value);
		}

		private double _TotalProfitPercentage;
		public double TotalProfitPercentage
		{
			get => _TotalProfitPercentage;
			set => Set(ref _TotalProfitPercentage, value);
		}
		#endregion

		#region Methods
		private void CalculateStatistics()
		{
			if (SelectedMember == null)
			{
				this.TotalIncome = this.CalculateTotalIncome();
				this.TotalElectricity = new ElectricityPaymentSharesRepository(new BaseDataContext()).GetAll().Sum(p => p.Amount);
				this.TotalExpenses = new DepositsRepository(new BaseDataContext()).GetAll().Sum(p => p.Amount);
			}
			else
			{
				this.TotalIncome = this.CalculateTotalIncome(SelectedMember);
				this.TotalElectricity = new ElectricityPaymentSharesRepository(new BaseDataContext()).GetAll().Where(p => p.Member.Id == SelectedMember.Id).Sum(p => p.Amount);
				this.TotalExpenses = new DepositsRepository(new BaseDataContext()).GetAll().Where(d => d.Member.Id == SelectedMember.Id) .Sum(p => p.Amount);
			}

			this.TotalProfit = TotalIncome - TotalExpenses - TotalElectricity;
			this.TotalProfitPercentage = Math.Abs(Math.Round(TotalIncome / (TotalExpenses + TotalElectricity) * 100, 2));
		}

		private double CalculateTotalIncome(Member member = null)
		{
			double income = 0;
			List<Currency> currencies = new List<Currency>(new CurrenciesRepository(new BaseDataContext()).GetAll());
			List<PayoutShare> payoutShares = new List<PayoutShare>(new PayoutSharesRepository(new BaseDataContext()).GetAll());

			WebClient webClient = new WebClient();
			webClient.BaseAddress = $"https://api.binance.com/api/v3/ticker/price";

			var json = webClient.DownloadString(webClient.BaseAddress);
			List<dynamic> obj = JsonConvert.DeserializeObject<List<object>>(json);
			foreach (var currency in currencies)
			{
				dynamic curr = obj.Find(o => o["symbol"] == $"{currency.ShortName}RUB");
				if (member == null)
					income += (double)curr["price"] * payoutShares.Where(p => p.BaseEntity.Currency.Id == currency.Id).Sum(p => p.Amount);
				else
					income += (double)curr["price"] * payoutShares.Where(p => p.BaseEntity.Currency.Id == currency.Id).Where(p => p.Member.Id == member.Id) .Sum(p => p.Amount);
			}

			return Math.Round(income, 0);
		}
		#endregion
	}
}
