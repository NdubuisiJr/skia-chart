# skia-chart
A simple xamarin.Forms chart library, built on top of the skiasharp.views.forms library. This library is developed with engineering and statistics in mind. It might not be the prettiest but it will visualize your data appropriately.

## Current Features
* Line Chart.
* Plots multiple line chart on the same axis.

<p>
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1580448959.png" width="350" title="Single line"> 
</p
## Code Example
  XAML-page
  
  ```
  <?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d"
             xmlns:chartview="clr-namespace:SkiaChart.Views;assembly=SkiaChart"
             xmlns:chart="clr-namespace:SkiaChart.Charts;assembly=SkiaChart"
             xmlns:viewModels="clr-namespace:TestApp.ViewModels"
             x:Class="TestApp.Views.LineChartView"
             >
    <ContentPage.BindingContext>
        <viewModels:LineChartViewModel/>
    </ContentPage.BindingContext>
    
    <chartview:ChartCanvas x:TypeArguments="chart:LineChart"
                           x:Name="chartView" 
                           Chart="{Binding Chart}"
                           GridLines="10"
                           />
</ContentPage>
  ```
  
  View Model
  
  ```
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
        }

        private IEnumerable<LineChart> GenerateLineCharts() {
            var linear = new LineChart(GetXValues(), GetYValuesLinearly());
            linear.ChartColor = SKColors.Red;

            var random1 = new LineChart(GetXValues(), Random(300).OrderBy(x=>x));
            random1.ChartColor = SKColors.Green;

            var random2 = new LineChart(GetXValues(), Random(50).OrderBy(x=>x));
            random2.ChartColor = SKColors.Yellow;
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
                yield return 500 - (1 - (float)rand.NextDouble() * (500 - lowerLimit));
            }
        }

        private IEnumerable<float> GetYValuesLinearly() {
            for (int i = 0; i < 500; i++) {
                yield return i + 1;
            }
        }

        public Chart<LineChart> Chart { get; set; }
    }
}
  
  ``` 
<p>
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585258834.png" width="350" title="Multiple lines">
</p>

## Getting Started

* Fork.
* Clone.
* Restore nuget packages.
* Run the Test App.
* Contribute!

## Authors

* **Ndubuisi Jr Chukuigwe** - *Initial work* - [NdubuisiJr](https://github.com/NdubuisiJr)


## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/NdubuisiJr/skia-chart/blob/master/LICENSE) file for details


