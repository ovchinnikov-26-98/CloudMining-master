using CloudMining.Infrastructure.Commands;
using CloudMining.Views;
using System;
using System.Windows.Input;

namespace CloudMining.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		#region Properties
		private int _menuIndex;
		public int MenuIndex
		{
			get => _menuIndex;
			set => Set(ref _menuIndex, value);
		}

		private object _mainContentControl;
		public object MainContentControl
		{
			get => _mainContentControl;
			set => Set(ref _mainContentControl, value);
		}
		#endregion

		#region Constructor
		public MainWindowViewModel()
		{
			ChangeMenuIndexCommand = new RelayCommand(OnChangeMenuIndexCommandExecuted, CanChangeMenuIndexCommandExecute);
		}
		#endregion

		#region Commands

		#region ChangeMenuIndex
		public ICommand ChangeMenuIndexCommand { get; }
		private bool CanChangeMenuIndexCommandExecute(object p) => true;
		private void OnChangeMenuIndexCommandExecuted(object p)
		{
			MenuIndex = Convert.ToInt32(p);

			switch (MenuIndex)
			{
				case 1:
						MainContentControl = new StatisticsView();
					break;
				case 2:
						MainContentControl = new MembersView();
					break;
				case 3:
						MainContentControl = new PayoutsView();
					break;
				case 4:
						MainContentControl = new DepositsView();
					break;
				case 5:
						MainContentControl = new PurchasesView();
					break;
				case 6:
						MainContentControl = new ElectricityPaymentsView();
					break;
			}
		}
		#endregion

		#endregion
	}
}