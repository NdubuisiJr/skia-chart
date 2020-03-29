using SkiaChart.Helpers;
using SkiaSharp;

namespace SkiaChart.Models {
    //A class that wraps the canvas with additional information
    public class CanvasWrapper {
        internal CanvasWrapper(SKCanvas canvas, SKRect chartArea, int gridLines,
            int height, int width, Converter converter) {
            Canvas = canvas;
            ChartArea = chartArea;
            GridLines = gridLines;
            DeviceHeight = height;
            DeviceWidth = width;
            Converter = converter;
        }

        public SKCanvas Canvas { get; }
        public SKRect ChartArea { get; }
        public int DeviceHeight { get; }
        public int DeviceWidth { get; }
        public int GridLines { get; }
        internal Converter Converter { get; }

        public int NumberOfCharts { get; set; }
        public int NumberPlottedChart { get; set; }
    }
}
