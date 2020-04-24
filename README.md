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

## Gallery - With Legend and Title
<p>
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1586234593.png" width="350"> 
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1586234609.png" width="350">
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1586234621.png" width="350">
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1587333013.png" width="350">
</p>

## Gallery - Without Legend and Title
<p>
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585258837.png" width="350"> 
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585492508.png" width="350"> 
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585603936.png" width="350">
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585849946.png" width="350"> 
</p>

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
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585258837.png" width="350" title="Line Chart">
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
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585492508.png" width="350" title="Bar chart">
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
  <img src="https://github.com/NdubuisiJr/skia-chart/blob/master/ProjectFiles/Screenshot_1585603936.png" width="350" title="Scatter chart">
</p>

## Getting Started

* Fork.
* Clone.
* Restore nuget packages.
* Run the Test App.
* Contribute!

## Author

* **Ndubuisi Jr Chukuigwe** - *Initial work* - [NdubuisiJr](https://github.com/NdubuisiJr)

## Contributors
<a href="https://github.com/NdubuisiJr/skia-chart/graphs/contributors">
  <p>
     <img src="https://avatars3.githubusercontent.com/u/44238346?v=4" width="50"/>
  </p>
</a>


## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/NdubuisiJr/skia-chart/blob/master/LICENSE) file for details


