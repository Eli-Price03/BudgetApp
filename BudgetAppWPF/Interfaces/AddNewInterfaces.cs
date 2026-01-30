using BudgetAppWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAppWPF.Interfaces
{
    public interface IAddNew
    {
        public void AddNewExpense(ExpenseModel expense);

        public void AddNewIncome(IncomeModel income);
    }
}
