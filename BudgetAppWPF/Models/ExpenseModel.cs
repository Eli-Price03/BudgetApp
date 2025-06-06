using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppWPF.Models
{
	public class ExpenseModel
	{
		public int? Id { get; set; }
		public string Title { get; set; }
		public decimal Amount { get; set; }
	}
}
