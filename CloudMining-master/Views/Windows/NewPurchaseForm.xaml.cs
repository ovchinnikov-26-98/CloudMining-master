using CloudMining.Models;
using System;
using System.Windows;

namespace CloudMining.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для NewPurchaseForm.xaml
	/// </summary>
	public partial class NewPurchaseForm : Window
	{
		private readonly Purchase _NewPurchase;

		public NewPurchaseForm(Purchase NewPurchase)
		{
			InitializeComponent();

			this._NewPurchase = NewPurchase;
		}

		private void AddPurchaseButton_Click(object sender, RoutedEventArgs e)
		{
			string newPurchaseSubject = SubjectTextBox.Text;
			double newPurchaseAmount = Convert.ToDouble(AmountTextBox.Text);
			DateTime newPurchaseDate = PurchaseDatePicker.SelectedDate.Value;
			bool isMandatory = IsMandatoryCheckBox.IsChecked.Value;

			if (!newPurchaseSubject.Equals(null) && newPurchaseAmount > 0
				&& newPurchaseDate <= DateTime.Now)
			{
				this._NewPurchase.Subject = newPurchaseSubject;
				this._NewPurchase.Amount = newPurchaseAmount;
				this._NewPurchase.Date = newPurchaseDate;
				this._NewPurchase.IsMandatory = isMandatory;

				this.DialogResult = true;
				MessageBox.Show("Покупка добавлена!");
			}
			else
				MessageBox.Show("Введите корректные данные!");
		}
	}
}
