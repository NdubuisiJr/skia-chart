using SkiaChart;
using SkiaChart.Charts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp.ViewModels {
    public class LineChartViewModel {
        public LineChartViewModel() {
            Chart = new Chart<LineChart>(GenerateLineCharts()) {
                YTitle = "Y-Axis Title",
                XTitle = "X-Axis Title"
            };
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

            var linear3 = new LineChart(GetXValues(), GetYValuesLinearly()) {
                ChartColor = SKColors.Blue
            };

            var random4 = new LineChart(GetXValues(), Random(10).OrderBy(x => x)) {
                ChartColor = SKColors.Brown
            };

            var random5 = new LineChart(GetXValues(), Random(50).OrderBy(x => x)) {
                ChartColor = SKColors.Pink
            };

            var random6 = new LineChart(GetXValues(), Random(10).OrderBy(x => x)) {
                ChartColor = SKColors.DarkOrange
            };

            var random7 = new LineChart(GetXValues(), Random(50).OrderBy(x => x)) {
                ChartColor = SKColors.Purple
            };
            return new List<LineChart> { linear, random1, linear3, random2,random4,random5,random6,random7 };
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
