using BudgetAppWPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppWPF.DataAccess
{
	public class DataAccess : DbContext
	{
		// sql lite db
		static string folder = $@"C\Users\{Environment.UserName}\AppData\BudgetApp";
		static string db = $@"{folder}\BudgetAppDB.db";
		// db sets
		public DbSet<IncomeModel> Incomes { get; set; }
		public DbSet<ExpenseModel> Expenses { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source={db}");
			base.OnConfiguring(optionsBuilder);
		}

		public void CreateFolder()
		{
			if (Directory.Exists(folder) == false)
			{
				Directory.CreateDirectory(folder);
			}
		}

		public DataAccess()
		{
			
		}
	}
}
