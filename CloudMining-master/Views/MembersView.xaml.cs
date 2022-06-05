using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CloudMining.Views
{
	/// <summary>
	/// Логика взаимодействия для MembersView.xaml
	/// </summary>
	public partial class MembersView : UserControl
	{
		public MembersView()
		{
			InitializeComponent();

			this.dataGrid.Items.SortDescriptions.Add(new SortDescription("DepositsAmount", ListSortDirection.Descending));
		}
	}
}
