using SkiaChart.Helpers;
using SkiaSharp;

namespace SkiaChart.Models {
    //A class that wraps the canvas with additional information
    public class CanvasWrapper {
        internal CanvasWrapper(SKCanvas canvas, SKRect chartArea, int gridLines,
            int height, int width, bool canShowLegend, Converter converter) {
            Canvas = canvas;
            ChartArea = chartArea;
            GridLines = gridLines;
            DeviceHeight = height;
            DeviceWidth = width;
            Converter = converter;
            CanShowLegend = canShowLegend;
        }

        public SKCanvas Canvas { get; }
        public SKRect ChartArea { get; }
        public int DeviceHeight { get; }
        public int DeviceWidth { get; }
        public int GridLines { get; }
        public bool CanShowLegend { get; }
        internal Converter Converter { get; }
        public int NumberOfCharts { get; set; }
        public int NumberPlottedChart { get; set; }
    }
}
