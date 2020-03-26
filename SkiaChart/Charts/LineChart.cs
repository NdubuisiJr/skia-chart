using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkiaChart.Charts {
    public class LineChart : IChart {
        public LineChart(IEnumerable<float> xValues, IEnumerable<float> yValues) {
            ValidateTable(xValues, yValues);
            UpdateDateType<float, float>();
            OriginalData = GenerateXYPoints(xValues, yValues);
        }

        public LineChart(IEnumerable<string> xValues, IEnumerable<float> yValues) {
            ValidateTable(xValues, yValues);
            UpdateDateType<string, float>();
            OriginalData = DistributeXGenerateYPoints(xValues, yValues);
        }

        public void RenderChart(SKCanvas canvas) {
            var firstPoint = ConstructionData.ElementAt(0);
            var chartPath = new SKPath();
            chartPath.MoveTo(firstPoint);
            for (int index = 1; index < ConstructionData.Count(); index++) {
                chartPath.LineTo(ConstructionData.ElementAt(index));
            }
            canvas.DrawPath(chartPath, _chartPaint);
        }

        public string GetXLabel(float labelValue) {
            if (XLabel == null || !XLabel.Any()) {
                return labelValue.ToString();
            }

            var index = int.Parse(Math.Round(labelValue, 0).ToString());
            return XLabel[index];
        }

        public string GetYLabel(float labelValue) {
            if (YLabel == null || !YLabel.Any()) {
                return labelValue.ToString();
            }

            var index = int.Parse(Math.Round(labelValue, 0).ToString());
            return YLabel[index];
        }

        private IEnumerable<SKPoint> GenerateXYPoints(IEnumerable<float> xValues, IEnumerable<float> yValues) {
            var numberOfPoints = xValues.Count();
            var points = new List<SKPoint>();
            for (int i = 0; i < numberOfPoints; i++) {
                points.Add(new SKPoint(xValues.ElementAt(i), yValues.ElementAt(i)));
            }
            return points;
        }

        private IEnumerable<SKPoint> DistributeXGenerateYPoints(IEnumerable<string> xValues, IEnumerable<float> yValues) {
            var numberOfPoints = xValues.Count();
            XLabel = new List<string>();
            var points = new List<SKPoint>();
            for (int i = 0; i < numberOfPoints; i++) {
                points.Add(new SKPoint(i, yValues.ElementAt(i)));
                XLabel.Add(xValues.ElementAt(i));
            }
            return points;
        }

        private void UpdateDateType<TxValues, TyValues>() {
            XValueType = typeof(TxValues);
            YValueType = typeof(TyValues);
        }

        private static void ValidateTable<TxValues, TyValues>(IEnumerable<TxValues> xValues, IEnumerable<TyValues> yValues) {
            if (xValues == null || yValues == null) {
                throw new ArgumentNullException("xValues and yValues must not be null");
            }

            if (xValues.Count() != yValues.Count()) {
                throw new ArgumentException("Number of xValues must equal number of yValues");
            }
        }

        public IEnumerable<SKPoint> ConstructionData { get; set; }
        public IEnumerable<SKPoint> OriginalData { get; set; }
        public List<string> XLabel { get; set; }
        public List<string> YLabel { get; set; }
        public Type XValueType { get; set; }
        public Type YValueType { get; set; }

        private SKColor _chartColor;
        public SKColor ChartColor {
            get => _chartColor;
            set {
                if (value != _chartColor) {
                    _chartColor = value;
                    _chartPaint.Color = value;
                }
            }
        }

        private float _strokeWidth;
        public float StrokeWidth {
            get=> _strokeWidth;
            set{
                if (value != _strokeWidth) {
                    _strokeWidth = value;
                    _chartPaint.StrokeWidth = value;
                }
            }
        } 

        private SKPaint _chartPaint = new SKPaint() {
            IsAntialias = true,
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Green,
            StrokeWidth=5,
            StrokeCap = SKStrokeCap.Round
        };
    }
}
