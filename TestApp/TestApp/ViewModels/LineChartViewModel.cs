using SkiaChart;
using SkiaChart.Charts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp.ViewModels {
    public class LineChartViewModel {
        public LineChartViewModel() {
            Chart = new Chart<LineChart>(GenerateLineCharts());
            GridColor = SKColors.Blue;
        }

        private IEnumerable<LineChart> GenerateLineCharts() {
            var linear = new LineChart(GetXValues(), GetYValuesLinearly()) {
                ChartColor = SKColors.Red
            };

            var random1 = new LineChart(GetXValues(), Random(10).OrderBy(x => x)) {
                ChartColor = SKColors.Green
            };

            var random2 = new LineChart(GetXValues(), Random(50).OrderBy(x => x)) {
                ChartColor = SKColors.Yellow
            };
            return new List<LineChart> { linear, random1, random2 };
        }

        private IEnumerable<float> GetXValues() {
            for (int i = 0; i < 500; i++) {
                yield return i + 1;
            }
        }

        private IEnumerable<float> Random(int lowerLimit) {
            var rand = new Random();
            for (int i = 0; i < 500; i++) {
                yield return 500 - ((1 - (float)rand.NextDouble()) * (500 - lowerLimit));
            }
        }

        private IEnumerable<float> GetYValuesLinearly() {
            for (int i = 0; i < 500; i++) {
                yield return i + 1;
            }
        }

        public Chart<LineChart> Chart { get; set; }
        public SKColor GridColor { get; set; }
    }
}
