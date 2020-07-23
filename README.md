 [![Actions Status](https://github.com/NdubuisiJr/skia-chart/workflows/Build/badge.svg)](https://github.com/NdubuisiJr/skia-chart/actions)

# skia-chart
A simple xamarin.Forms chart library, built on top of the skiasharp.views.forms library. This library is developed with engineering and statistics in mind. It might not be the prettiest but it will visualize your data appropriately.

## Supported Charts
* Line Chart.
* Bar Chart.
* Scatter Chart.
* Area Chart.

## Features
* Horizontal axis title.
* Vertical axis title.
* Legend
* Point markers for line and area charts.
* Plots multiple line chart on the same axis.
* Plots multiple bars on the same axis.
* Plots multiple scatter points on the same axis.
* Plots multiple area charts on the same axis.

## Supported Platforms
* Android
* IOS
* UWP
* macOS
* GTK
* WPF

## Gallery - Display on Android, IOS, UWP, and macOS
<p>
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1586234593.png" width="175"> 
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1586234609.png" width="175">
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1586234621.png" width="175">
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1587333013.png" width="175">
 
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/iOS/area.png" width="175"> 
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/iOS/bar.png" width="175">
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/iOS/scatter.png" width="175">
  
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/UWP/area.png" width="175"> 
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/UWP/bar.png" width="175">
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/UWP/scatter.png" width="175">
  
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/macOS/area.png" width="175"> 
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/macOS/bar.png" width="175">
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/macOS/scatter.png" width="175">
    
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/WPF/line.png" width="175"> 
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/WPF/area.png" width="175"> 
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/WPF/bar.png" width="175">
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/WPF/scatter.png" width="175">
</p>

## Gallery - Without Legend and Title
<p>/
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585258837.png" width="175"> 
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585492508.png" width="175"> 
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585603936.png" width="175">
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585849946.png" width="175"> 
</p>

## Getting Started
* Open Nuget package manager for solution
* Search for SkiaChart.Forms
* Install on all the projects

## Code Example
### Line Chart
  XAML-page
  
  ```
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
    
    <chartview:ChartCanvas x:TypeArguments="chart:LineChart" x:Name="chartView" 
                           Chart="{Binding Chart}" GridLines="10" />
</ContentPage>

  ```  
 see <a href="https://github.com/NdubuisiJr/skia-chart/tree/master/TestApp/TestApp">Test App<a/> for typical bindings.
 
<p>
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585258837.png" width="175" title="Line Chart">
</p>

### Bar Chart
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
             x:Class="TestApp.Views.BarChartPage">   
    <ContentPage.BindingContext>
        <viewModels:BarChartViewModel/>
    </ContentPage.BindingContext>
    <chartview:ChartCanvas x:TypeArguments="chart:BarChart" x:Name="chartView" Chart="{Binding Chart}"
                           GridLines="10" GridColor="{Binding GridColor}" />
</ContentPage>
  ```
  see <a href="https://github.com/NdubuisiJr/skia-chart/tree/master/TestApp/TestApp">Test App<a/> for typical bindings.
<p>
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585492508.png" width="175" title="Bar chart">
</p>

### Scatter Chart
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
             x:Class="TestApp.Views.BarChartPage">   
    <ContentPage.BindingContext>
        <viewModels:ScatterChartViewModel/>
    </ContentPage.BindingContext>
    <chartview:ChartCanvas x:TypeArguments="chart:ScatterChart" x:Name="chartView" Chart="{Binding Chart}"
                           GridLines="10" GridColor="{Binding GridColor}" />
</ContentPage>
  ```
 see <a href="https://github.com/NdubuisiJr/skia-chart/tree/master/TestApp/TestApp">Test App<a/> for typical bindings.
<p>
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585603936.png" width="175" title="Scatter chart">
</p>

## Tutorials - Watch the YouTube video below:
[![Watch the video](https://img.youtube.com/vi/066CmRSQE0k/hqdefault.jpg)](https://youtu.be/066CmRSQE0k)
<br/> [Full tutorial series](https://www.youtube.com/watch?v=ExjwgJ3Y0z8&list=PL918TratNgZxkJGbr3t-NiIftscT5O_cq)

## Contributing

* Create a Fork from this repository.
* Clone your fork into your work station.
* Switch to the development branch.
* Make your changes on the development branch.
* Push your changes to your fork.
* Create a pull request back to the main repository.
* Add a new remote called upstream to point to the main repository.

## Author

* **Ndubuisi Jr Chukuigwe** - *Initial work* - [NdubuisiJr](https://github.com/NdubuisiJr)

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/NdubuisiJr/skia-chart/blob/master/LICENSE) file for details


