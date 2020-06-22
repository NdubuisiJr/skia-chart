using SkiaChart;
using SkiaChart.Charts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp.ViewModels
{
	public class LineChartViewModel
	{
		public LineChartViewModel()
		{
			Chart = new Chart<LineChart>(GenerateLineCharts())
			{
				YTitle = "Randomly generated values",
				XTitle = "Distributed values"
			};
			GridColor = SKColors.LightPink;
		}

		private IEnumerable<LineChart> GenerateLineCharts()
		{
			var linear = new LineChart(GetXValues(), GetYValuesLinearly())
			{
				ChartColor = SKColors.Red,
				ChartName = "Linear",
				ShowPoints = true
				, Width = 0.5f
			};

			var random1 = new LineChart(GetXValues(), Random(10).OrderBy(x => x))
			{
				ChartColor = SKColors.Green,
				ChartName = "Random starting from 10",
				ShowPoints = true
				, Width = 0.5f
			};

			var random2 = new LineChart(GetXValues(), Random(50).OrderBy(x => x))
			{
				ChartColor = SKColors.Yellow,
				ChartName = "Random starting from 50",
				ShowPoints = true
				, Width = 0.5f
			};

			var linear3 = new LineChart(GetXValues(), GetYValuesLinearly())
			{
				ChartColor = SKColors.Blue,
				ChartName = "Second Linear",
				ShowPoints = true
				, Width = 0.5f
			};

			return new List<LineChart> { linear, random1, linear3, random2 };
		}

		private IEnumerable<float> GetXValues()
		{
			for (int i = 0; i < 500; i++)
			{
				yield return i + 1;
			}
		}

		private IEnumerable<float> Random(int lowerLimit)
		{
			var rand = new Random();
			for (int i = 0; i < 500; i++)
			{
				yield return 500 - ((1 - (float)rand.NextDouble()) * (500 - lowerLimit));
			}
		}

		private IEnumerable<float> GetYValuesLinearly()
		{
			for (int i = 0; i < 500; i++)
			{
				yield return i + 1;
			}
		}

		public Chart<LineChart> Chart { get; set; }
		public SKColor GridColor { get; set; }
	}
}
