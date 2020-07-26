using SkiaChart;
using SkiaChart.Charts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace TestApp.ViewModels {
    public class DonutChartViewModel {
        public DonutChartViewModel() {
            Chart = new Chart<DonutChart>(GenerateLineCharts()) {
                YTitle = "Population values",
                XTitle = "Prediction curve values"
            };

            GridColor = SKColors.Gray;

            switch (Device.RuntimePlatform) {
                case Device.WPF:
                case Device.GTK:
                case Device.macOS:
                case Device.UWP: {
                        LabelTextSize = 15f;
                        LegendItemSpacing = 20f;
                        break;
                    }
                default: {
                        LabelTextSize = 30f;
                        LegendItemSpacing = 40f;
                        break;
                    }
            };
        }

        private IEnumerable<DonutChart> GenerateLineCharts() {
            var income = new DonutChart("Income", 2000) {
                ChartColor = SKColors.Red,
            };

            var expenditure = new DonutChart("Expenditure", 500) {
                ChartColor = SKColors.Green,
            };

            var profit = new DonutChart("Profit", 1500) {
                ChartColor = SKColors.DarkBlue,
            };
            return new List<DonutChart> { income, expenditure, profit };
        }


        public Chart<DonutChart> Chart { get; set; }
        public SKColor GridColor { get; set; }
        public float LabelTextSize { get; set; }
        public float LegendItemSpacing { get; set; }
    }
}
