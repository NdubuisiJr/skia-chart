using SkiaSharp;

namespace SkiaChart.Models {
    //A class that wraps the canvas with additional information
    public class CanvasWrapper {
        internal CanvasWrapper(SKCanvas canvas, SKRect chartArea) {
            Canvas = canvas;
            ChartArea = chartArea;
        }

        public SKCanvas Canvas { get; }
        public SKRect ChartArea { get; }
        public int NumberOfCharts { get; set; }
        public int NumberPlottedChart { get; set; }
    }
}
