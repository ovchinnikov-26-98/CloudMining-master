using System.ComponentModel;
using System.Windows.Controls;

namespace CloudMining.Views
{
	/// <summary>
	/// Логика взаимодействия для ElectricityPaymentsView.xaml
	/// </summary>
	public partial class ElectricityPaymentsView : UserControl
	{
		public ElectricityPaymentsView()
		{
			InitializeComponent();

			this.dataGrid.Items.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
		}
	}
}
