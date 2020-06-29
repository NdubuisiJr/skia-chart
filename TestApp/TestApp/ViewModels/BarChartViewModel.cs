using SkiaChart;
using SkiaChart.Charts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace TestApp.ViewModels {
    public class BarChartViewModel {
        public BarChartViewModel() {
            Chart = new Chart<BarChart>(GenerateLineCharts()) {
                YTitle = "Population values",
                XTitle = "Prediction curve values"
            };
            GridColor = SKColors.Gray;
        }

        private IEnumerable<BarChart> GenerateLineCharts() {
            var linear = new BarChart(GetXValues(), Random(100)) {
                ChartColor = SKColors.Red,
                ChartName = "Linear"
            };

            var random = new BarChart(GetXValues(), Random(100)) {
                ChartColor = SKColors.Green,
                ChartName = "Random"
            };
            return new List<BarChart> { linear, random };
        }

        private IEnumerable<string> GetXValues() {
            for (int i = 0; i < 15; i++) {
                yield return (i + 1).ToString();
            }
        }

        private IEnumerable<float> Random(int lowerLimit) {
            var rand = new Random();
            for (int i = 0; i < 15; i++) {
                yield return 500 - ((1 - (float)rand.NextDouble()) * (500 - lowerLimit));
            }
        }

        public Chart<BarChart> Chart { get; set; }
        public SKColor GridColor { get; set; }
    }
}
