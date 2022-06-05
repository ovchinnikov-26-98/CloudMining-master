using System.ComponentModel;
using System.Windows.Controls;

namespace CloudMining.Views
{
	/// <summary>
	/// Логика взаимодействия для DepositsView.xaml
	/// </summary>
	public partial class DepositsView : UserControl
	{
		public DepositsView()
		{
			InitializeComponent();

			this.dataGrid.Items.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
		}
	}
}
