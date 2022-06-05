using CloudMining.DataContext;
using CloudMining.Infrastructure.Commands;
using CloudMining.Models;
using CloudMining.Repositories;
using CloudMining.Repositories.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace CloudMining.ViewModels
{
	public class PayoutsViewModel : BaseViewModel
	{
		#region Constructor
		public PayoutsViewModel()
		{
			this._PayoutsRepository = new PayoutsRepository(new BaseDataContext());
			this._PayoutSharesRepository = new PayoutSharesRepository(new BaseDataContext());

			Payouts = new ObservableCollection<Payout>(_PayoutsRepository.GetAll());

			CompletePayoutShareCommand = new RelayCommand(OnCompletePayoutShareCommandExecuted, CanCompletePayoutShareCommandExecute);
		}
		#endregion

		#region Properties
		private readonly IRepository<Payout> _PayoutsRepository;
		private readonly IRepository<PayoutShare> _PayoutSharesRepository;

		private ObservableCollection<Payout> _Payouts;
		public ObservableCollection<Payout> Payouts
		{
			get => _Payouts;
			set => Set(ref _Payouts, value);
		}

		private ObservableCollection<PayoutShare> _PayoutShares;
		public ObservableCollection<PayoutShare> PayoutShares
		{
			get => _PayoutShares;
			set => Set(ref _PayoutShares, value);
		}

		private Payout _SelectedPayout;
		public Payout SelectedPayout
		{
			get => _SelectedPayout;
			set
			{
				Set(ref _SelectedPayout, value);
				this.PayoutShares = new ObservableCollection<PayoutShare>(_PayoutSharesRepository.GetAll().Where(p => p.BaseEntity.Id == SelectedPayout.Id));
			}
		}

		private PayoutShare _SelectedPayoutShare;
		public PayoutShare SelectedPayoutShare
		{
			get => _SelectedPayoutShare;
			set => Set(ref _SelectedPayoutShare, value);
		}
		#endregion

		#region Commands
		public ICommand CompletePayoutShareCommand { get; }
		private bool CanCompletePayoutShareCommandExecute(object p) => SelectedPayoutShare != null && SelectedPayoutShare.IsDone == false;
		private void OnCompletePayoutShareCommandExecuted(object p)
		{
			DialogResult dialogResult = MessageBox.Show($"Доля [{SelectedPayoutShare.Id}] действительно переведена участнику [{SelectedPayoutShare.Member.Name}]?",
														"Подтверждение перевода выплаты",
														MessageBoxButtons.YesNo);

			if (dialogResult == DialogResult.Yes)
			{
				this.SelectedPayoutShare.IsDone = true;
				this._PayoutSharesRepository.Update(SelectedPayoutShare.Id, SelectedPayoutShare);
			}
		}
		#endregion
	}
}
