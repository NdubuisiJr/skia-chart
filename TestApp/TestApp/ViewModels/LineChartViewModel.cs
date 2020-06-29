using SkiaChart;
using SkiaChart.Charts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

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
			GridColor = SKColors.LightBlue;
		}

		private IEnumerable<LineChart> GenerateLineCharts()
		{
			var linear = new LineChart(GetXValues(), GetYValuesLinearly())
			{
				ChartColor = SKColors.Red,
				ChartName = "Linear",
				ShowPoints = true
			};
			switch (Device.RuntimePlatform)
			{
				case Device.UWP:
					{
						linear.LabelTextSize = 15;
						break;
					}
				default:
					{
						linear.LabelTextSize = 30;
						break;
					}
			};

			var random1 = new LineChart(GetXValues(), Random(10).OrderBy(x => x))
			{
				ChartColor = SKColors.Green,
				ChartName = "Random starting from 10",
				ShowPoints = true
			};

			var random2 = new LineChart(GetXValues(), Random(50).OrderBy(x => x))
			{
				ChartColor = SKColors.DarkBlue,
				ChartName = "Random starting from 50",
				ShowPoints = true
			};

			var linear3 = new LineChart(GetXValues(), GetYValuesLinearly())
			{
				ChartColor = SKColors.Blue,
				ChartName = "Second Linear",
				ShowPoints = true
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
