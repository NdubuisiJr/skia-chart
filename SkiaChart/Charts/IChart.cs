using SkiaSharp;
using System;
using System.Collections.Generic;

namespace SkiaChart.Charts {
    public interface IChart {
        void RenderChart(SKCanvas canvas);
        string GetXLabel(float labelValue);
        string GetYLabel(float labelValue);
        IEnumerable<SKPoint> ConstructionData { get; set; }
        IEnumerable<SKPoint> OriginalData { get; set; }
        List<string> XLabel { get; set; }
        List<string> YLabel { get; set; }
        Type XValueType { get; set; }
        Type YValueType { get; set; }
        SKColor ChartColor { get; set; }
        float StrokeWidth { get; set; }
    }
}
