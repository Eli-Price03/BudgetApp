using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BudgetAppWPF.Logic
{
	public class NumberValidation : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if(decimal.TryParse(value.ToString(), out _))
			{
				return ValidationResult.ValidResult;
			}
			else
			{
				return new ValidationResult(false, "This field can only contain numbers.");
			}
		}
	}
}
