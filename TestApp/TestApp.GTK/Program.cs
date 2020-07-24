using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace TestApp.GTK
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Gtk.Application.Init();
            Forms.Init();

            var app = new App();
            var window = new FormsWindow();
            window.LoadApplication(app);
            window.SetApplicationTitle("SkiaChart test app");
            window.Show();

            Gtk.Application.Run();
        }
    }
}
