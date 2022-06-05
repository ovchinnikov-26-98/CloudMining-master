using CloudMining.DataContext;
using CloudMining.Infrastructure.Commands;
using CloudMining.Models;
using CloudMining.Repositories;
using CloudMining.Repositories.Base;
using CloudMining.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace CloudMining.ViewModels
{
	public class DepositsViewModel : BaseViewModel
	{
		#region Constructor
		public DepositsViewModel()
		{
			this._DepositsRepository = new DepositsRepository(new BaseDataContext());
			this.Deposits = new ObservableCollection<Deposit>(_DepositsRepository.GetAll());

			AddNewDepositCommand = new RelayCommand(OnAddNewDepositCommandExecuted, CanAddNewDepositCommandExecute);
			RemoveDepositCommand = new RelayCommand(OnRemoveDepositCommandExecuted, CanRemoveDepositCommandExecute);
		}
		#endregion

		#region Properties
		private readonly IRepository<Deposit> _DepositsRepository;

		private ObservableCollection<Deposit> _Deposits;
		public ObservableCollection<Deposit> Deposits
		{
			get => _Deposits;
			set => Set(ref _Deposits, value);
		}

		private Deposit _SelectedDeposit;
		public Deposit SelectedDeposit
		{
			get => _SelectedDeposit;
			set => Set(ref _SelectedDeposit, value);
		}
		#endregion

		#region Commands
		public ICommand AddNewDepositCommand { get; }
		private bool CanAddNewDepositCommandExecute(object p) => true;
		private void OnAddNewDepositCommandExecuted(object p)
		{
			var newDeposit = new Deposit();
			var newForm = new NewDepositForm(newDeposit);

			if (newForm.ShowDialog() == true)
			{
				this._DepositsRepository.Create(newDeposit);
				this._Deposits.Add(newDeposit);
				this.SelectedDeposit = newDeposit;
			}
		}

		public ICommand RemoveDepositCommand { get; }
		private bool CanRemoveDepositCommandExecute(object p) => SelectedDeposit != null;
		private void OnRemoveDepositCommandExecuted(object p)
		{
			DialogResult dialogResult = MessageBox.Show($"Вы действительно хотите удалить депозит участника {SelectedDeposit.Member.Name} на сумму {SelectedDeposit.Amount}р.?",
														"Удаления депозита",
														MessageBoxButtons.YesNo);

			if (dialogResult == DialogResult.Yes)
			{
				this._DepositsRepository.Delete(SelectedDeposit.Id);
				this.Deposits.Remove(SelectedDeposit);

				MessageBox.Show("Депозит удален.");
			}
		}
		#endregion
	}
}
