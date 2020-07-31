using SkiaChart.Axes;
using SkiaChart.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;

namespace SkiaChart.Interfaces {
    public interface IChart {
        void RenderChart(CanvasWrapper canvas, Axis axis, IMinMax minMax);
        List<SKPoint> ConstructionData { get; }
        IEnumerable<SKPoint> OriginalData { get; set; }
        List<string> XLabel { get;}
        List<string> YLabel { get; }
        Type XValueType { get; }
        Type YValueType { get; }
        SKColor ChartColor { get; set; }
    }
}
