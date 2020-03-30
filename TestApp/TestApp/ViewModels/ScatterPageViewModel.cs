using SkiaChart;
using SkiaChart.Charts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.ViewModels {
    public class ScatterPageViewModel {
        public ScatterPageViewModel() {
            Chart = new Chart<ScatterChart>(GenerateLineCharts());
            GridColor = SKColors.LightGray;
        }

        private IEnumerable<ScatterChart> GenerateLineCharts() {
            var random = new ScatterChart(GetXValues(), Random(5)) {
                ChartColor = SKColors.Red,
                IsStroked=true
            };

            var random1 = new ScatterChart(GetXValues(), Random(20)) {
                ChartColor = SKColors.Green
            };

            var random2 = new ScatterChart(GetXValues(), Random(40)) {
                ChartColor = SKColors.Yellow,
                IsStroked = true
            };
            return new List<ScatterChart> { random, random1, random2 };
        }

        private IEnumerable<float> GetXValues() {
            for (int i = 0; i < 50; i++) {
                yield return i + 1;
            }
        }

        private IEnumerable<float> Random(int lowerLimit) {
            var rand = new Random();
            for (int i = 0; i < 50; i++) {
                yield return 50 - ((1 - (float)rand.NextDouble()) * (50 - lowerLimit));
            }
        }

        public Chart<ScatterChart> Chart { get; set; }
        public SKColor GridColor { get; set; }
    }
}
