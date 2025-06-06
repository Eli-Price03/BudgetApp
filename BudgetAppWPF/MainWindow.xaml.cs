using BudgetAppWPF.Models;
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
using BudgetAppWPF.Logic;
using System.Collections.ObjectModel;
using BudgetAppWPF.Charts;

namespace BudgetAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
			AppStart();
        }
		// in memory data store
		public ObservableCollection<IncomeModel> income = new();
		public ObservableCollection<ExpenseModel> expenses = new();
		public decimal totalincome = 0;
		public decimal totalexpenses = 0;
		public decimal remainder = 0;
		DataAccess.DataAccess db = new BudgetAppWPF.DataAccess.DataAccess();

		// startup logic

		public void AppStart()
		{
			db.CreateFolder();
			HelperClass.CreateDB(db);
			var dbexpenses = HelperClass.LoadExpenses();
			var dbincome = HelperClass.LoadIncome();
			if (dbexpenses.Count == 0)
			{
				AddSampleExpense();
			}
			else
			{
				foreach (var expense in dbexpenses)
				{
					expenses.Add(expense);
				}
			}
			if (dbincome.Count == 0)
			{
				AddSampleIncome();
			}
			else
			{
				foreach (var i in dbincome)
				{
					income.Add(i);
				}
			}
			CalculateIncome();
			CalculateExpenses();
			CalculateRemainder();
			DataGrid_Expenses.ItemsSource = expenses;
			DataGrid_Income.ItemsSource = income;
			PopulatePieChart();
			piechart_stackpanel.DataContext = PieChart;
		}

		private void PopulatePieChart()
		{
			PieChart.DataContext = PieChart;
				var pie = new PieChart(expenses.ToList()).Chart;
			PieChart.Model = pie;
		}

		#region left hand menu click events
		private void btn_dashboard_Click(object sender, RoutedEventArgs e)
		{
			MainTabControl.SelectedItem = tab_dashboard;
			CalculateRemainder();
			PopulatePieChart();
		}

		private void btn_income_Click(object sender, RoutedEventArgs e)
		{
			MainTabControl.SelectedItem = tab_income;
		}

		private void btn_expenses_Click(object sender, RoutedEventArgs e)
		{
			MainTabControl.SelectedItem = tab_expenses;
		}
		#endregion

		// add new / delete button clicks
		private void btn_addNewExpense_Click(object sender, RoutedEventArgs e)
		{
			AddSampleExpense();
		}

		private void btn_addNewIncome_Click(object sender, RoutedEventArgs e)
		{
			AddSampleIncome();
		}

		private void btn_DeleteIncome_Click(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			if(button != null)
			{
				var item = button.DataContext;
				int row = DataGrid_Income.Items.IndexOf(item);
				if (income[row].Id == null)
				{
					income.Remove(income[row]);
				}
				else
				{
					income[row].DeleteIncome();
					income.Remove(income[row]);
				}
			}
			CalculateIncome();
		}

		private void btn_DeleteExpense_Click(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			if (button != null)
			{
				var item = button.DataContext;
				int row = DataGrid_Expenses.Items.IndexOf(item);
				if (expenses[row].Id == null)
				{
					expenses.Remove(expenses[row]);
				}
				else
				{
					expenses[row].DeleteExpense();
					expenses.Remove(expenses[row]);
				}
			}
			CalculateExpenses();
		}

		private void AddSampleIncome()
		{
			income.Add(new IncomeModel { Amount = 10, Id = null, Title = "Sample" });
			CalculateIncome() ;
		}

		private void AddSampleExpense()
		{
			expenses.Add(new ExpenseModel { Amount = 10, Id = null, Title = "Sample"});
			CalculateExpenses();
		}

		private void CalculateIncome()
		{
			totalincome = income.ToList().CalculateTotalIncome();
			text_incometotal.Text = $"Total Income: £{totalincome.ToString()}";
			text_totalincome.Text = $"Total Income: £{totalincome.ToString()}";
		}
		private void CalculateExpenses()
		{
			totalexpenses = expenses.ToList().CalculateTotalExpense();
			text_expenses.Text = $"Total Expenses: £{totalexpenses.ToString()}";
			text_totalexpenses.Text = $"Total Expenses: £{totalexpenses.ToString()}";
		}
		private void CalculateRemainder()
		{
			remainder = totalincome - totalexpenses;
			text_remainder.Text = $"Remainder: £{remainder.ToString()}";
		}

		private void DataGrid_Income_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			if(e.EditingElement is TextBox t)
			{
				BindingExpression b = t.GetBindingExpression(TextBox.TextProperty);
				b?.UpdateSource();
			}
			CalculateIncome();
		}

		private void DataGrid_Expenses_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			if (e.EditingElement is TextBox t)
			{
				BindingExpression b = t.GetBindingExpression(TextBox.TextProperty);
				b?.UpdateSource();
			}
			CalculateExpenses();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SaveAll();
		}

		private void SaveAll()
		{
			HelperClass.SaveExpenses(expenses.ToList());
			HelperClass.SaveIncome(income.ToList());
		}
	}
}