using BudgetAppWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetAppWPF.DataAccess;

namespace BudgetAppWPF.Logic
{
	public static class HelperClass
	{

		// create db 
		
		public static void CreateDB(DataAccess.DataAccess db)
		{
			SQLitePCL.Batteries.Init();
			db.Database.EnsureCreated();
		}

		// income / expense calculations
		public static decimal CalculateTotalIncome(this List<IncomeModel> input)
		{
			return input.Sum(x => x.Amount);
		}

		public static decimal CalculateTotalExpense(this List<ExpenseModel> input)
		{
			return input.Sum(x => x.Amount);
		}

		// income data access

		public static void SaveNewIncome(this IncomeModel model)
		{
			using(var context = new DataAccess.DataAccess())
			{
				context.Incomes.Add(model);
				context.SaveChanges();
			}
		}

		public static void UpdateIncome(this IncomeModel model)
		{
			using (var context = new DataAccess.DataAccess())
			{
				context.Incomes.Update(model);
				context.SaveChanges();
			}
		}

		public static List<IncomeModel> LoadIncome()
		{
			using(var context = new DataAccess.DataAccess())
			{
				return context.Incomes.ToList();
			}
		}

		public static void DeleteIncome(this IncomeModel model)
		{
			using(var context = new DataAccess.DataAccess())
			{
				context.Incomes.Remove(model);
				context.SaveChanges();
			}
		}

		// expenses data access

		public static void SaveNewExpense(this ExpenseModel model)
		{
			using (var context = new DataAccess.DataAccess())
			{
				context.Expenses.Add(model);
				context.SaveChanges();
			}
		}

		public static void UpdateExpense(this ExpenseModel model)
		{
			using (var context = new DataAccess.DataAccess())
			{
				context.Expenses.Update(model);
				context.SaveChanges();
			}
		}

		public static List<ExpenseModel> LoadExpenses()
		{
			using (var context = new DataAccess.DataAccess())
			{
				return context.Expenses.ToList();
			}
		}

		public static void DeleteExpense(this ExpenseModel model)
		{
			using (var context = new DataAccess.DataAccess())
			{
				context.Expenses.Remove(model);
				context.SaveChanges();
			}
		}

		public static void SaveIncome(List<IncomeModel> input)
		{
			using(var context = new DataAccess.DataAccess())
			{

				foreach (var income in input)
				{
					if(income.Id == null)
					{
						income.SaveNewIncome();
					}
					else
					{
						income.UpdateIncome();
					}
				}
			}
		}

		public static void SaveExpenses(List<ExpenseModel> input)
		{
			using(var context = new DataAccess.DataAccess())
			{
				foreach(var expense in input)
				{
					if(expense.Id == null)
					{
						expense.SaveNewExpense();
					}
					else
					{
						expense.UpdateExpense();
					}
				}
			}
		}

	}
}
