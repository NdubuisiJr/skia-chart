using SkiaChart.Axes;
using SkiaChart.Enums;
using SkiaChart.Interfaces;
using SkiaChart.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkiaChart.Charts {
    /// <summary>
    /// Handles the rendering of donut shaped chart
    /// </summary>
    public class RadialChart : ChartBase, ISingleValueChart {

        public RadialChart(string label, float value) {
            var xLabel = new List<string> { label };
            var yValue = new List<float> { value };
            ValidateInputs(xLabel, yValue);
            UpdateDataType<string, float>();
            OriginalData = DistributeXGenerateYPoints(xLabel, yValue);
        }

        public override void RenderChart(CanvasWrapper canvasWrapper, Axis axis, IMinMax minMax) {
            var chartArea = canvasWrapper.ChartArea;
            var canvas = canvasWrapper.Canvas;

            var radius = Math.Min(chartArea.MidX, chartArea.MidY) / 2.5f;
            radius = radius - (canvasWrapper.NumberPlottedChart * StrokeWidth);

            _chartPaint.StrokeWidth = StrokeWidth;
            _chartPaint.Style = SKPaintStyle.Stroke;

            var teta = 360 - ((minMax.Ymax - OriginalData.ElementAt(0).Y) / (minMax.Ymax - minMax.Ymin) * 360);
            
            var rect = new SKRect(chartArea.MidX - radius, chartArea.MidY - radius, chartArea.MidX + radius, chartArea.MidY + radius);
            canvas.DrawArc(rect, 90, -teta, false, _chartPaint);

            _chartPaint.Color = ChartColor.WithAlpha(70);
            canvas.DrawCircle(chartArea.MidX, chartArea.MidY, radius, _chartPaint);
            canvasWrapper.NumberPlottedChart += 1;

            _chartPaint.Color = ChartColor;
            if (canvasWrapper.CanShowLegend) {
                RenderLegend(canvasWrapper, axis, canvas, PointPlotVariant.ScatterChart);
            }
        }

        public float StrokeWidth { get; set; } = 30;

    }
}
