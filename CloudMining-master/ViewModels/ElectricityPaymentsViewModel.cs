using CloudMining.DataContext;
using CloudMining.Infrastructure.Commands;
using CloudMining.Models;
using CloudMining.Repositories;
using CloudMining.Repositories.Base;
using CloudMining.Views.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace CloudMining.ViewModels
{
	public class ElectricityPaymentsViewModel : BaseViewModel
	{
		#region Constructor
		public ElectricityPaymentsViewModel()
		{
			this._ElectricityPaymentsRepository = new ElectricityPaymentsRepository(new BaseDataContext());
			this._ElectricityPaymentSharesRepository = new ElectricityPaymentSharesRepository(new BaseDataContext());

			this.ElectricityPayments = new ObservableCollection<ElectricityPayment>(this._ElectricityPaymentsRepository.GetAll());

			AddElectricityPaymentCommand = new RelayCommand(OnAddElectricityPaymentCommandExecuted, CanAddElectricityPaymentCommandExecute);
			CompleteElectricityPaymentShareCommand = new RelayCommand(OnCompleteElectricityPaymentShareCommandExecuted, CanCompleteElectricityPaymentShareCommandExecute);
		}
		#endregion

		#region Properties
		private readonly IRepository<ElectricityPayment> _ElectricityPaymentsRepository;
		private readonly IRepository<ElectricityPaymentShare> _ElectricityPaymentSharesRepository;

		private ObservableCollection<ElectricityPayment> _ElectricityPayments;
		public ObservableCollection<ElectricityPayment> ElectricityPayments
		{
			get => _ElectricityPayments;
			set => Set(ref _ElectricityPayments, value);
		}

		private ObservableCollection<ElectricityPaymentShare> _ElectricityPaymentShares;
		public ObservableCollection<ElectricityPaymentShare> ElectricityPaymentShares
		{
			get => _ElectricityPaymentShares;
			set => Set(ref _ElectricityPaymentShares, value);
		}

		private ElectricityPaymentShare _SelectedElectricityPaymentShare;
		public ElectricityPaymentShare SelectedElectricityPaymentShare
		{
			get => _SelectedElectricityPaymentShare;
			set => Set(ref _SelectedElectricityPaymentShare, value);
		}

		private ElectricityPayment _SelectedElectricityPayment;
		public ElectricityPayment SelectedElectricityPayment
		{
			get => _SelectedElectricityPayment;
			set
			{
				Set(ref _SelectedElectricityPayment, value);
				this.ElectricityPaymentShares = new ObservableCollection<ElectricityPaymentShare>(_ElectricityPaymentSharesRepository.GetAll().Where(p => p.BaseEntity.Id == _SelectedElectricityPayment.Id));
			}
		}
		#endregion

		#region Commands
		public ICommand AddElectricityPaymentCommand { get; }
		private bool CanAddElectricityPaymentCommandExecute(object p) => true;
		private void OnAddElectricityPaymentCommandExecuted(object p)
		{
			ElectricityPayment newPayment = new ElectricityPayment();
			NewElectricityPaymentForm newForm = new NewElectricityPaymentForm(newPayment);

			if (newForm.ShowDialog() == true)
			{
				this._ElectricityPaymentsRepository.Create(newPayment);
				this.ElectricityPayments.Add(newPayment);
			}
		}

		public ICommand CompleteElectricityPaymentShareCommand { get; }
		private bool CanCompleteElectricityPaymentShareCommandExecute(object p) => SelectedElectricityPaymentShare != null && SelectedElectricityPaymentShare.IsDone == false;
		private void OnCompleteElectricityPaymentShareCommandExecuted(object p)
		{
			DialogResult dialogResult = MessageBox.Show($"Участник [{SelectedElectricityPaymentShare.Member.Name}] действительно перевел [{SelectedElectricityPaymentShare.Amount}р.] за электричество?",
														"Подтверждение перевода платежа за электричество",
														MessageBoxButtons.YesNo);

			if (dialogResult == DialogResult.Yes)
			{
				this.SelectedElectricityPaymentShare.IsDone = true;
				this._ElectricityPaymentSharesRepository.Update(SelectedElectricityPaymentShare.Id, SelectedElectricityPaymentShare);
			}
		}
		#endregion
	}
}
