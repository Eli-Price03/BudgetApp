using BudgetAppWPF.Interfaces;
using BudgetAppWPF.Models;
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

namespace BudgetAppWPF.Views
{
    /// <summary>
    /// Interaction logic for AddNew.xaml
    /// </summary>
    public partial class AddNew : Window, IAddNew
    {
        public Enums.Type _type;
        public IAddNew _parent;
        public AddNew(IAddNew parent, Enums.Type type)
        {
            InitializeComponent();
            _parent = parent;
            _type = type;
			SetTitle();
        }

		#region add new for interface - unused on this side
		public void AddNewExpense(ExpenseModel expense)
		{
			throw new NotImplementedException();
		}

		public void AddNewIncome(IncomeModel income)
		{
			throw new NotImplementedException();
		}
		#endregion

		private void btn_addNew_Click(object sender, RoutedEventArgs e)
		{
			AddNewEntry();
			CloseScreen();
		}

		private void btn_cancel_Click(object sender, RoutedEventArgs e)
		{
			CloseScreen();
		}

		private void AddNewEntry()
		{
			switch (_type)
			{
				case Enums.Type.Income:
					_parent.AddNewIncome(new IncomeModel { Title = tbx_title.Text, Amount = decimal.Parse(tbx_amount.Text) }); break;
				case Enums.Type.Expense:
					_parent.AddNewExpense(new ExpenseModel { Title = tbx_title.Text, Amount = decimal.Parse(tbx_amount.Text) }); break;
			}
		}

		private void CloseScreen()
		{
			this.Close();
		}

		private void SetTitle()
		{
			if(_type == Enums.Type.Income)
			{
				text_title.Text = "Add New Income";
			}
			else
			{
				text_title.Text = "Add New Expense";
			}
		}
	}
}
