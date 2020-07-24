using SkiaChart.Helpers;
using SkiaSharp;

namespace SkiaChart.Models {
    //A class that wraps the canvas with additional information
    public class CanvasWrapper {
        internal CanvasWrapper(SKCanvas canvas, SKRect chartArea, int gridLines,
            int height, int width, bool thisIsiOSOrAndroid, bool canShowLegend, float legendItemSpacing, float labelTextSize,
                Converter converter) {
            Canvas = canvas;
            ChartArea = chartArea;
            GridLines = gridLines;
            DeviceHeight = height;
            DeviceWidth = width;
            ThisIsiOSOrAndroid = thisIsiOSOrAndroid;
            Converter = converter;
            CanShowLegend = canShowLegend;
            LegendItemSpacing = legendItemSpacing;
            LegendDrawingStartX = chartArea.Left;
            LabelTextSize = labelTextSize;
        }

        public SKCanvas Canvas { get; }
        public SKRect ChartArea { get; }
        public int DeviceHeight { get; }
        public int DeviceWidth { get; }
        public int GridLines { get; }
        public bool ThisIsiOSOrAndroid { get; }
        public bool CanShowLegend { get; }
        public float LegendItemSpacing { get; }
        internal Converter Converter { get; }
        public int NumberOfCharts { get; set; }
        public int NumberPlottedChart { get; set; }
        public int NumberOfDrawnLegend { get; set; }
        public float LegendDrawingStartX { get; set; }
        public float LabelTextSize { get; }
        internal bool drawYTickMarkOnBars = true;
    }
}
