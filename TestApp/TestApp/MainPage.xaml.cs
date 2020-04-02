using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Views;
using Xamarin.Forms;

namespace TestApp {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }

        private async void lineButton_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new LineChartView());
        }

        private async void barChart_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new BarChartPage());
        }

        private async void scatterChart_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new ScatterPage());
        }

        private async void AreaChart_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new AreaChartPage());
        }
    }
}
