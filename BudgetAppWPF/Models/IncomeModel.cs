using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppWPF.Models
{
	public class IncomeModel
	{
		public int? Id { get; set; }
		public decimal Amount { get; set; }
		public string Title { get; set; }
	}
}
