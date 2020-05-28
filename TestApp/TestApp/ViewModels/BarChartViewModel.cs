using SkiaChart;
using SkiaChart.Charts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace TestApp.ViewModels {
    public class BarChartViewModel : INotifyPropertyChanged {
        public BarChartViewModel() {
            CreateCommand = new Command(CreateAction);
            GridColor = SKColors.Black;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CreateAction(object obj) {

            Chart = new Chart<BarChart>(GenerateBarCharts()) {
                YTitle = "Randomly generated values",
                XTitle = "Distributed values"
            };
        }

        private IEnumerable<BarChart> GenerateBarCharts() {
            var bar1 = new BarChart(GetXValues(), Random(1)) {
                ChartColor = SKColors.Green,
                ChartName="Random starting from 1"
            };
            var bar2 = new BarChart(GetXValues(), Random(5)) {
                ChartColor = SKColors.Red,
                ChartName = "Random starting from 5"
            };
            var bar3 = new BarChart(GetXValues(), Random(10)) {
                ChartColor = SKColors.Yellow,
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
        public SKColor GridColor { get; set; }
       
    }
}
