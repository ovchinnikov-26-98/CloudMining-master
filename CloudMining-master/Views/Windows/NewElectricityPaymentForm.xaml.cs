using CloudMining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CloudMining.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для NewElectricityPaymentForm.xaml
	/// </summary>
	public partial class NewElectricityPaymentForm : Window
	{
		private readonly ElectricityPayment newElectricityPayment;

		public NewElectricityPaymentForm(ElectricityPayment newElectricityPayment)
		{
			InitializeComponent();

			this.newElectricityPayment = newElectricityPayment;
		}

		private void AddElectricityPaymentButton_Click(object sender, RoutedEventArgs e)
		{
			DateTime newElectricityPaymentDate = Convert.ToDateTime(ElectricityPaymentDatePicker.SelectedDate.Value);
			double newElectricityPaymentAmount = Convert.ToDouble(ElectricityPaymentAmountTextBox.Text);

			if (newElectricityPaymentDate <= DateTime.Now && !newElectricityPaymentAmount.Equals(0))
			{
				this.newElectricityPayment.Date = newElectricityPaymentDate;
				this.newElectricityPayment.Amount = newElectricityPaymentAmount;

				this.DialogResult = true;
				MessageBox.Show("Платеж создан!");
			}
			else
				MessageBox.Show("Введите корректные данные!");
		}
	}
}
