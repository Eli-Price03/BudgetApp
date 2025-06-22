using BudgetAppWPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppWPF.Logic
{
	public static class CSVExport
	{
		public static void ExportToCSV(string filepath, List<ExpenseModel> expenses, List<IncomeModel> incomes)
		{
			CreateFile(filepath);
			using(StreamWriter sw = new StreamWriter(filepath))
			{
				foreach(var expense in expenses)
				{
					sw.WriteLine($"{expense.Title},{expense.Amount.ToString()},");
				}
				foreach (var income in incomes)
				{
					sw.WriteLine($"{income.Title},{income.Amount.ToString()},");
				}
			}
		}

		private static void CreateFile(string filepath)
		{
			if (File.Exists(filepath) == false)
			{
				File.Create(filepath).Close();
			}
		}
	}
}
