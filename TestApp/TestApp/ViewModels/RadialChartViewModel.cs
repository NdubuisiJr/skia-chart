using SkiaChart;
using SkiaChart.Charts;
using SkiaSharp;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TestApp.ViewModels {
    public class RadialChartViewModel {
        public RadialChartViewModel() {
            Chart = new Chart<RadialChart>(GenerateLineCharts()) {
                YTitle = "Population values",
                XTitle = "Prediction curve values",
                Ymax=2500,
                Ymin=0
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

        private IEnumerable<RadialChart> GenerateLineCharts() {
            var income = new RadialChart("Income", 2000) {
                ChartColor = SKColors.Red,
            };

            var expenditure = new RadialChart("Expenditure", 500) {
                ChartColor = SKColors.Green,
            };

            var profit = new RadialChart("Profit", 1500) {
                ChartColor = SKColors.DarkBlue,
            };

            return new List<RadialChart> { income, expenditure, profit ,
            income, expenditure, profit ,
            income, expenditure, profit ,
            income, expenditure, profit };
        }


        public Chart<RadialChart> Chart { get; set; }
        public SKColor GridColor { get; set; }
        public float LabelTextSize { get; set; }
        public float LegendItemSpacing { get; set; }
    }
}
