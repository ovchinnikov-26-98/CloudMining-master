using CloudMining.DataContext;
using CloudMining.Infrastructure.Commands;
using CloudMining.Models;
using CloudMining.Repositories;
using CloudMining.Repositories.Base;
using CloudMining.Views.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace CloudMining.ViewModels
{
	public class PurchasesViewModel : BaseViewModel
	{
		#region Constructor
		public PurchasesViewModel()
		{
			this._PurchasesRepository = new PurchasesRepository(new BaseDataContext());
			this._PurchaseSharesRepository = new PurchaseSharesRepository(new BaseDataContext());

			this.Purchases = new ObservableCollection<Purchase>(_PurchasesRepository.GetAll());

			AddPurchaseCommand = new RelayCommand(OnAddPurchaseCommandExecuted, CanAddPurchaseCommandExecute);
			RemovePurchaseCommand = new RelayCommand(OnRemovePurchaseCommandExecuted, CanRemovePurchaseCommandExecute);
			CompletePurchaseShareCommand = new RelayCommand(OnCompletePurchaseShareCommandExecuted, CanCompletePurchaseShareCommandExecute);
		}
		#endregion

		#region Properties
		private IRepository<Purchase> _PurchasesRepository;
		private IRepository<PurchaseShare> _PurchaseSharesRepository;

		private ObservableCollection<Purchase> _Purchases;
		public ObservableCollection<Purchase> Purchases
		{
			get => _Purchases;
			set => Set(ref _Purchases, value);
		}

		private ObservableCollection<PurchaseShare> _PurchaseShares;
		public ObservableCollection<PurchaseShare> PurchaseShares
		{
			get => _PurchaseShares;
			set => Set(ref _PurchaseShares, value);
		}

		private Purchase _SelectedPurchase;
		public Purchase SelectedPurchase
		{
			get => _SelectedPurchase;
			set
			{
				Set(ref _SelectedPurchase, value);
				PurchaseShares = new ObservableCollection<PurchaseShare>(this._PurchaseSharesRepository.GetAll().Where(p => p.BaseEntity.Id == SelectedPurchase.Id));
			}
		}

		private PurchaseShare _SelectedPurchaseShare;
		public PurchaseShare SelectedPurchaseShare
		{
			get => _SelectedPurchaseShare;
			set => Set(ref _SelectedPurchaseShare, value);
		}
		#endregion

		#region Commands
		public ICommand AddPurchaseCommand { get; }
		private bool CanAddPurchaseCommandExecute(object p) => true;
		private void OnAddPurchaseCommandExecuted(object p)
		{
			var newPurchase = new Purchase();
			var newForm = new NewPurchaseForm(newPurchase);

			if (newForm.ShowDialog() == true)
			{
				this._PurchasesRepository.Create(newPurchase);
				this._Purchases.Add(newPurchase);
				this.SelectedPurchase = newPurchase;
			}
		}

		public ICommand RemovePurchaseCommand { get; }
		private bool CanRemovePurchaseCommandExecute(object p) => SelectedPurchase != null;
		private void OnRemovePurchaseCommandExecuted(object p)
		{
			DialogResult dialogResult = MessageBox.Show($"Вы действительно хотите удалить покупку {SelectedPurchase.Id} на сумму {SelectedPurchase.Amount}р.?",
														"Удаления покупки",
														MessageBoxButtons.YesNo);

			if (dialogResult == DialogResult.Yes)
			{
				this._PurchasesRepository.Delete(SelectedPurchase.Id);
				this.Purchases.Remove(SelectedPurchase);

				MessageBox.Show("покупка удалена.");
			}
		}

		public ICommand CompletePurchaseShareCommand { get; }
		private bool CanCompletePurchaseShareCommandExecute(object p) => SelectedPurchaseShare != null && SelectedPurchaseShare.IsDone == false;
		private void OnCompletePurchaseShareCommandExecuted(object p)
		{
			DialogResult dialogResult = MessageBox.Show($"Участник [{SelectedPurchaseShare.Member.Name}] действительно перевел [{SelectedPurchaseShare.Amount}р.] для покупки [{SelectedPurchaseShare.BaseEntity.Subject}]?",
														"Подтверждение перевода платежа для покупки",
														MessageBoxButtons.YesNo);

			if (dialogResult == DialogResult.Yes)
			{
				this.SelectedPurchaseShare.IsDone = true;
				this._PurchaseSharesRepository.Update(SelectedPurchaseShare.Id, SelectedPurchaseShare);
				(new DepositsRepository(new BaseDataContext())).Create(
					new Deposit
					{
						Amount = this.SelectedPurchaseShare.Amount,
						Comment = this.SelectedPurchaseShare.BaseEntity.Subject,
						Date = DateTime.Now,
						Member = SelectedPurchaseShare.Member
					});
			}
		}
		#endregion
	}
}
