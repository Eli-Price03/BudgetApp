using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetAppWPF.Models;
using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.Wpf;

namespace BudgetAppWPF.Charts
{
	public class PieChart
	{
		public PlotModel Chart { get; private set; }

		public PieChart(List<ExpenseModel> input)
		{
			Chart = new PlotModel { Title = "Expense Chart", SubtitleFontSize = 10, TitleFontSize = 24, };
			PieSeries pie = new PieSeries();
			foreach(var i in input)
			{
				pie.Slices.Add(new PieSlice(i.Title, (double)i.Amount));
			}
			pie.OutsideLabelFormat = "{1} {2:0.#}%";
			pie.InsideLabelFormat = "£{0}";
			pie.AreInsideLabelsAngled = true;
			Chart.PlotMargins = new OxyThickness(10);
			Chart.Background = OxyColors.Transparent;
			Chart.PlotAreaBackground = OxyColors.Transparent;
			Chart.PlotAreaBorderColor = OxyColors.Transparent;
			Chart.PlotAreaBorderThickness = new OxyThickness(0);
			Chart.Series.Add(pie);
			Chart.Padding = new OxyThickness(10);
			
		}
		public PieChart()
		{
			PieSeries pie = new PieSeries { Title = "Expenses Pie Chart"};
			Chart = new PlotModel();
			pie.Slices.Add(new PieSlice("Example", 20));
			pie.Slices.Add(new PieSlice("Example", 80));
			Chart.Series.Add(pie);
		}
	}
}
