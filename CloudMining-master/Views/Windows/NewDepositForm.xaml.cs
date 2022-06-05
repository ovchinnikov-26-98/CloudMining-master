using CloudMining.DataContext;
using CloudMining.Models;
using CloudMining.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CloudMining.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для NewDepositForm.xaml
    /// </summary>
    public partial class NewDepositForm : Window
    {
		private readonly Deposit _NewDeposit;
		private readonly List<Member> _Members;

        public NewDepositForm(Deposit newDeposit)
        {
            InitializeComponent();

			this._NewDeposit = newDeposit;
			this._Members = new MembersRepository(new BaseDataContext()).GetAll().ToList();

			MembersComboBox.ItemsSource = this._Members;
			MembersComboBox.DisplayMemberPath = "Name";
        }

		private void AddDepositButton_Click(object sender, RoutedEventArgs e)
		{
			Member newDepositMember = _Members.FirstOrDefault(m => m.Name == MembersComboBox.Text);
			double newDepositAmount = Convert.ToDouble(AmountTextBox.Text);
			DateTime newDepositDate = DepositDatePicker.SelectedDate.Value;
			string newDepositComment = CommentTextBox.Text;

			if (!newDepositMember.Equals(null) && !newDepositAmount.Equals(String.Empty)
				&& newDepositDate <= DateTime.Now)
			{
				this._NewDeposit.Member = newDepositMember;
				this._NewDeposit.Amount = newDepositAmount;
				this._NewDeposit.Date = newDepositDate;
				this._NewDeposit.Comment = newDepositComment;

				this.DialogResult = true;
				MessageBox.Show("Депозит добавлен!");
			}
			else
				MessageBox.Show("Введите корректные данные!");
		}
	}
}
