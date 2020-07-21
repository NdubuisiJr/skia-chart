using SkiaChart;
using SkiaChart.Charts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp.ViewModels {
    public class TestViewModel : INotifyPropertyChanged {
        public TestViewModel() {
            CreateCommand = new Command(CreateAction);
        }

        private async Task CreateCharts() {
            await Task.Run(() => {
                BarChart = new Chart<BarChart>(GenerateBarCharts());
            });
        }
        private Chart<BarChart> _barchart;
        public Chart<BarChart> BarChart {
            get => _barchart;
            set {
                if (_barchart != value) {
                    _barchart = value;
                    RaisePropertyChanged(nameof(BarChart));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void CreateAction(object obj) {
            await CreateCharts();
            //Chart = new Chart<BarChart>(GenerateBarCharts()) {
            //    YTitle = "Randomly generated values",
            //    XTitle = "Distributed values"
            //};
        }

        private IEnumerable<BarChart> GenerateBarCharts() {
            var bar1 = new BarChart(GetXValues(), Random(1)) {
                ChartColor = SKColors.Green,
                ChartName = "Random starting from 1"
            };

            switch (Device.RuntimePlatform)
            {
                case Device.GTK:
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

            var bar2 = new BarChart(GetXValues(), Random(5)) {
                ChartColor = SKColors.Red,
                ChartName = "Random starting from 5"
            };
            var bar3 = new BarChart(GetXValues(), Random(10)) {
                ChartColor = SKColors.DarkBlue,
                ChartName = "Random starting from 10"
            };
            return new List<BarChart> { bar1, bar2, bar3 };
        }

        private IEnumerable<float> Random(int lowerLimit) {
            var rand = new Random();
            for (int i = 0; i < 10; i++) {
                yield return 15 - ((1 - (float)rand.NextDouble()) * (15 - lowerLimit));
            }
        }

        private IEnumerable<string> GetXValues() {
            for (int i = 0; i < 10; i++) {
                yield return $"{i + 1}value";
            }
        }

        private void RaisePropertyChanged(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public Command CreateCommand { get; }
        private Chart<BarChart> _chart;
        public Chart<BarChart> Chart {
            get => _chart;
            set {
                if (_chart != value) {
                    _chart = value;
                    RaisePropertyChanged(nameof(Chart));
                }
            }
        }
        public SKColor BarchartColor { get; }
        = SKColor.Parse("#2196F3");
        public SKColor GridColor { get; }
                = SKColor.Parse("#2196F3");
		public float LabelTextSize { get; set; }
		public float LegendItemSpacing { get; set; }
    }
}
