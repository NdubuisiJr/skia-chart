using SkiaChart.Axes;
using SkiaChart.Enums;
using SkiaChart.Interfaces;
using SkiaChart.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SkiaChart.Charts {
    /// <summary>
    /// Handles the rendering of donut shaped chart
    /// </summary>
    public class RadialChart : ChartBase, ISingleValueChart {

        public RadialChart(string label, float value) {
            var xLabel = new List<string> { label };
            var yValue = new List<float> { value };
            Label = label;
            Value = value;
            ValidateInputs(xLabel, yValue);
            UpdateDataType<string, float>();
            OriginalData = DistributeXGenerateYPoints(xLabel, yValue);
        }

        public override void RenderChart(CanvasWrapper canvasWrapper, Axis axis, IMinMax minMax) {
            var divisor = 2.5f;
            var strokeWidth = 30;
    
            if (canvasWrapper.NumberOfCharts>6) {
                switch (Device.RuntimePlatform) {
                    case Device.WPF:
                    case Device.GTK:
                    case Device.macOS:
                    case Device.UWP: {
                            divisor = 2.0f;
                            strokeWidth = 15;
                            break;
                        }
                    default: {
                            divisor = 1.5f;
                            strokeWidth = 30;
                            break;
                        }
                };
            }


            var chartArea = canvasWrapper.ChartArea;
            var canvas = canvasWrapper.Canvas;

            var radius = Math.Min(chartArea.MidX, chartArea.MidY) / divisor;
            radius = radius - (canvasWrapper.NumberPlottedChart * strokeWidth);

            _chartPaint.StrokeWidth = strokeWidth;
            _chartPaint.Style = SKPaintStyle.Stroke;

            var teta = 360 - ((minMax.Ymax - OriginalData.ElementAt(0).Y) / (minMax.Ymax - minMax.Ymin) * 360);

            var rect = new SKRect(chartArea.MidX - radius, chartArea.MidY - radius, chartArea.MidX + radius, chartArea.MidY + radius);
            var path = new SKPath();
            path.AddArc(rect, 90, -teta);
            canvas.DrawPath(path, _chartPaint);
            
            _chartPaint.Color = ChartColor.WithAlpha(70);
            canvas.DrawCircle(chartArea.MidX, chartArea.MidY, radius, _chartPaint);
            canvasWrapper.NumberPlottedChart += 1;

            _chartPaint.Color = ChartColor;
            ChartName = $"{Label} : {Value}";
            RenderLegend(canvasWrapper, axis, canvas, PointPlotVariant.ScatterChart);
        }

        //No initial Calculation for this chart type
        public virtual float InitialCalculations(IMinMax minMax) {
            return 0f;
        }

        public string Label { get; }
        public float Value { get; }
    }
}
