using SkiaChart.Axes;
using SkiaChart.Enums;
using SkiaChart.Interfaces;
using SkiaChart.Models;
using SkiaSharp;
using System;
using System.Linq;
using Xamarin.Forms;

namespace SkiaChart.Charts {
    public class DonutChart : RadialChart, ISingleValueChart {
        public DonutChart(string label, float value) : base(label, value) { }

        public override void RenderChart(CanvasWrapper canvasWrapper, Axis axis, IMinMax minMax) {
            int strokeWidth;
            float divisor;
            switch (Device.RuntimePlatform) {
                case Device.WPF:
                case Device.GTK:
                case Device.macOS:
                case Device.UWP: {
                        divisor = 3.0f;
                        strokeWidth = 100;
                        break;
                    }
                default: {
                        divisor = 2.0f;
                        strokeWidth = 200;
                        break;
                    }
            };

            var chartArea = canvasWrapper.ChartArea;
            var canvas = canvasWrapper.Canvas;
            _chartPaint.StrokeWidth = strokeWidth;
            _chartPaint.Style = SKPaintStyle.Stroke;
            _chartPaint.StrokeCap = SKStrokeCap.Butt;


            var radius = Math.Min(chartArea.MidX, chartArea.MidY) / divisor;
            var rect = new SKRect(chartArea.MidX - radius, chartArea.MidY - radius, chartArea.MidX + radius, chartArea.MidY + radius);
            var startAngle = canvasWrapper.NumberPlottedChart == 0 ? 0 : canvasWrapper.NextStartAngle;
            var sweepAngle = (Angel / canvasWrapper.SumOfAngles) * 360;
            canvas.DrawArc(rect, -startAngle, -sweepAngle, false, _chartPaint);

            canvasWrapper.NextStartAngle = startAngle + sweepAngle;
            canvasWrapper.NumberPlottedChart += 1;
            ChartName = $"{Label} : {Value}";
            RenderLegend(canvasWrapper, axis, canvas, PointPlotVariant.ScatterChart);
        }

        public override float InitialCalculations(IMinMax minMax) {
            Angel = 360 - ((minMax.Ymax - OriginalData.ElementAt(0).Y) / (minMax.Ymax - minMax.Ymin) * 360);
            return Angel;
        }

        public float Angel { get; private set; }
    }
}
