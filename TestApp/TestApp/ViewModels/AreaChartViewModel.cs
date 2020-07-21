using SkiaChart;
using SkiaChart.Charts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace TestApp.ViewModels {
    public class AreaChartViewModel {
        public AreaChartViewModel() {
            Chart = new Chart<AreaChart>(GenerateLineCharts()) {
                YTitle = "Population values",
                XTitle = "Prediction curve values"
            };
            
            GridColor = SKColors.Gray;

            switch (Device.RuntimePlatform)
            {
                case Device.GTK:
                case Device.macOS:
                case Device.UWP:
                    {
                        LabelTextSize = 15f;
                        LegendItemSpacing = 20f;
                        break;
                    }
                default:
                    {
                        LabelTextSize = 30f;
                        LegendItemSpacing = 40f;
                        break;
                    }
            };
        }

        private IEnumerable<AreaChart> GenerateLineCharts() {
            var linear = new AreaChart(GetXValues(), GetYValuesLinearly()) {
                ChartColor = SKColors.Red,
                ShowPoints=true,
                ChartName="Linear"
            };

            var random = new AreaChart(GetXValues(), Random(100).OrderBy(x=>x)) {
                ChartColor = SKColors.Green,
                ShowPoints = true,
                ChartName = "Random"
            };

            var trigValues = Trigonometric();
            var sineCurve = new AreaChart(trigValues.Item1, trigValues.Item2) {
                ChartColor = SKColors.DarkBlue,
                ShowPoints = true,
                ChartName = "Trigonometric"
            };
            return new List<AreaChart> { linear, random, sineCurve };
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

        private Tuple<IEnumerable<float>,IEnumerable<float>> Trigonometric() {
            var yValues = new List<float>();
            var xValues = new List<float>();
            for (int i = 0; i < 150; i++) {
                double x = i * Math.PI;
                double y = 200 + 100 * Math.Sin(x/10);
                xValues.Add((float)x);
                yValues.Add((float)y);
            }
            return new Tuple<IEnumerable<float>, IEnumerable<float>>(xValues, yValues );
        }

        public Chart<AreaChart> Chart { get; set; }
        public SKColor GridColor { get; set; }
		public float LabelTextSize { get; set; }
        public float LegendItemSpacing { get; set; }
    }
}
