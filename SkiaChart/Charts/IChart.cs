using SkiaChart.Axes;
using SkiaChart.Interfaces;
using SkiaChart.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;

namespace SkiaChart.Charts {
    public interface IChart {
        void RenderChart(CanvasWrapper canvas, Axis axis, IMinMax minMax);
        IEnumerable<SKPoint> ConstructionData { get; set; }
        IEnumerable<SKPoint> OriginalData { get; set; }
        List<string> XLabel { get; set; }
        List<string> YLabel { get; set; }
        Type XValueType { get; set; }
        Type YValueType { get; set; }
        SKColor ChartColor { get; set; }
        float Width { get; set; }
    }
}
