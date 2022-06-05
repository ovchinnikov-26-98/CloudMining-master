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
	/// Логика взаимодействия для PurchasesView.xaml
	/// </summary>
	public partial class PurchasesView : UserControl
	{
		public PurchasesView()
		{
			InitializeComponent();

			this.dataGrid.Items.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
		}
	}
}
