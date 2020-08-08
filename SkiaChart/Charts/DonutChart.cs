using SkiaChart.Axes;
using SkiaChart.Enums;
using SkiaChart.Interfaces;
using SkiaChart.Models;
using SkiaSharp;
using System;
using System.Linq;

namespace SkiaChart.Charts {
    public class DonutChart : RadialChart, ISingleValueChart {
        public DonutChart(string label, float value) : base(label,value) {}

        public override void RenderChart(CanvasWrapper canvasWrapper, Axis axis, IMinMax minMax) {
            var divisor = 3.5f;
            var strokeWidth = 60;
            var chartArea = canvasWrapper.ChartArea;
            var canvas = canvasWrapper.Canvas;
            var lastPlottedChart = canvasWrapper.LastPlottedChart;
            _chartPaint.StrokeWidth = strokeWidth;
            _chartPaint.Style = SKPaintStyle.Stroke;


            var radius = Math.Min(chartArea.MidX, chartArea.MidY) / divisor;
            var rect = new SKRect(chartArea.MidX - radius, chartArea.MidY - radius, chartArea.MidX + radius, chartArea.MidY + radius);
            var startAngle = lastPlottedChart is null? 90 : ((DonutChart)lastPlottedChart).SweepAngle;
            var sweepAngle = (Angel / canvasWrapper.SumOfAngles) * 360;
            canvas.DrawArc(rect, startAngle, -sweepAngle, false, _chartPaint);

            canvasWrapper.LastPlottedChart = this;
            ChartName = $"{Label} : {Value}";
            RenderLegend(canvasWrapper, axis, canvas, PointPlotVariant.ScatterChart);
        }

        public override float InitialCalculations(IMinMax minMax) {
            Angel = 360 - ((minMax.Ymax - OriginalData.ElementAt(0).Y) / (minMax.Ymax - minMax.Ymin) * 360);
            return Angel;
        }

        public float Angel { get; private set; }
        public float SweepAngle { get; private set; }
    }
}
