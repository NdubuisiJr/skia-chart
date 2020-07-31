using SkiaChart.Axes;
using SkiaChart.Enums;
using SkiaChart.Exceptions;
using SkiaChart.Interfaces;
using SkiaChart.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using static SkiaChart.Helpers.Constants;

namespace SkiaChart.Charts {
    /// <summary>
    /// Base class for the implementation of different chart types
    /// </summary>
    public abstract class ChartBase : IChart {

        //Keeps track of the X-Y data types
        protected void UpdateDataType<TxValues, TyValues>() {
            XValueType = typeof(TxValues);
            YValueType = typeof(TyValues);
        }

        // Validates the inputed X-Y values
        protected void ValidateInputs<TxValues, TyValues>(
            IEnumerable<TxValues> xValues, IEnumerable<TyValues> yValues) {
            if (xValues == null || yValues == null) {
                throw new ArgumentNullException("xValues and yValues must not be null");
            }

            if (xValues.Count() != yValues.Count()) {
                throw new ArgumentException("Number of xValues must equal number of yValues");
            }
        }

        //Ensures that charts are constructed through the Chart<T> class
        protected void CheckConstructionPolicy(string chartName) {
            if (ConstructionData == null || ConstructionData.Count() < 1) {
                throw new SelfConstructionException(chartName);
            }
        }

        //Generates points for drawing (graphics points) given the X-Y values
        protected IEnumerable<SKPoint> GenerateXYPoints(IEnumerable<float> xValues,
            IEnumerable<float> yValues) {
            var numberOfPoints = xValues.Count();
            var points = new List<SKPoint>();
            for (int i = 0; i < numberOfPoints; i++) {
                points.Add(new SKPoint(xValues.ElementAt(i), yValues.ElementAt(i)));
            }
            return points;
        }

        //Generates points for drawing (graphics points) by distributing X from 0 to count
        //and Y for give YValues
        protected IEnumerable<SKPoint> DistributeXGenerateYPoints(IEnumerable<string> xValues,
            IEnumerable<float> yValues) {
            var numberOfPoints = xValues.Count();
            XLabel = new List<string>(xValues);
            var points = new List<SKPoint>();
            for (int i = 0; i < numberOfPoints; i++) {
                points.Add(new SKPoint(i, yValues.ElementAt(i)));
            }
            return points;
        }

        //Generates points for drawing (graphics points) by distributing Y from 0 to count
        //and X for give YValues
        protected IEnumerable<SKPoint> DistributeYGenerateXPoints(
            IEnumerable<float> xValues, IEnumerable<string> yValues) {
            var numberOfPoints = xValues.Count();
            YLabel = new List<string>();
            var points = new List<SKPoint>();
            for (int i = 0; i < numberOfPoints; i++) {
                points.Add(new SKPoint(xValues.ElementAt(i), i));
                YLabel.Add(yValues.ElementAt(i));
            }
            return points;
        }
        /// <summary>
        /// Gets the X-Axis Label given the corresponding floating point value
        ///  for that axis.
        /// </summary>
        /// <param name="labelValue">Floating point number representation of label</param>
        /// <returns></returns>
        public string GetXLabel(float labelValue) {
            if (XLabel == null || !XLabel.Any()) {
                return labelValue.ToString();
            }

            var index = int.Parse(Math.Round(labelValue, 0).ToString());
            return XLabel[index];
        }

        /// <summary>
        /// Gets the Y-Axis Label given the corresponding floatin point value 
        /// for that axis.
        /// </summary>
        /// <param name="labelValue">Floating point number representation of label</param>
        /// <returns></returns>
        public string GetYLabel(float labelValue) {
            if (YLabel == null || !YLabel.Any()) {
                return string.Format("{0, 9}", labelValue.ToString("N2"));
            }

            var index = int.Parse(Math.Round(labelValue, 0).ToString());
            return YLabel[index];
        }

        protected void RenderLegend(CanvasWrapper canvasWrapper, Axis axis, SKCanvas canvas,
            PointPlotVariant plotVariant) {
            canvasWrapper.NumberOfDrawnLegend++;
            float start;
            if(canvasWrapper.ThisIsiOSOrAndroid) {
                start = canvasWrapper.ChartArea.Bottom + MarginFromChartToLegend;
            }
            else {
                start = (canvasWrapper.Converter.YOffset * 2) + MarginFromChartToLegend;
            }
            float end;
            float leftEdge;
            end = start + (canvasWrapper.LegendItemSpacing * canvasWrapper.NumberOfDrawnLegend);
            leftEdge = canvasWrapper.LegendDrawingStartX + LeftEdgeLegendMargin;

            float heightPaddingForText = 0;

            canvas.Save();
            axis.AntiOrientAxis(float.MaxValue);
            switch (plotVariant) {
                case PointPlotVariant.LineChart:
                    _chartPaint.IsStroke = false;
                    heightPaddingForText = 7;
                    canvas.DrawLine(leftEdge, end, canvasWrapper.LegendDrawingStartX + canvasWrapper.LegendItemSpacing,
                        end, _chartPaint);
                    break;
                case PointPlotVariant.ScatterChart:
                    _chartPaint.IsStroke = false;
                    heightPaddingForText = 7;
                    canvas.DrawCircle(leftEdge + DistanceToCenterOfLegendCircle,
                        end, 7, _chartPaint);
                    if (!canvasWrapper.ThisIsiOSOrAndroid) {
                        leftEdge += 10;
                    }
                    break;
                case PointPlotVariant.AreaChart:
                    heightPaddingForText = 15;
                    canvas.DrawRect(leftEdge, end, WidthOfLegendRect, HeightOfLegendRect
                        , _chartPaint);
                    if (!canvasWrapper.ThisIsiOSOrAndroid) {
                        leftEdge += WidthOfLegendRect;
                    }
                    break;
                default:
                    break;
            }
            canvas.Restore();

            axis.DrawAndPositionLegend(ChartName, end + heightPaddingForText,
                leftEdge + canvasWrapper.LegendItemSpacing, _chartPaint);

            if (canvasWrapper.NumberOfDrawnLegend == NumberOfLegendItem) {
                canvasWrapper.LegendDrawingStartX += canvasWrapper.ChartArea.Width / (NumberOfLegendItem + 1);
                canvasWrapper.NumberOfDrawnLegend = 0;
            }
        }
        //Draws the vertical labels
        protected void DrawVerticalLabels(CanvasWrapper canvasWrapper, Axis axis, IMinMax minMax) {
            var heightSpacing = (canvasWrapper.ChartArea.Bottom - canvasWrapper.ChartArea.Top)
                                                / canvasWrapper.GridLines;
            var heightHolder = heightSpacing;
            heightSpacing += canvasWrapper.Converter.YOffset;
            for (int i = 0; i < canvasWrapper.GridLines; i++) {
                var labelValue = canvasWrapper.Converter
                                              .YValueToRealScale(heightSpacing, minMax.Ymax, minMax.Ymin);
                _labelPaint.TextSize = canvasWrapper.LabelTextSize;
                axis.DrawAndPositionYTickMark(GetYLabel(labelValue), heightSpacing,
                    canvasWrapper.Converter.Rect.Left - (_labelPaint.TextSize * 2.5f), _labelPaint);
                heightSpacing += heightHolder;
            }
        }

        //Draws the horizontal labels
        protected void DrawHorizontalLabels(CanvasWrapper canvasWrapper, Axis axis, IMinMax minMax) {

            var widthSpacing = (canvasWrapper.ChartArea.Right - canvasWrapper.ChartArea.Left)
                                                    / canvasWrapper.GridLines;
            var widthHolder = widthSpacing;
            widthSpacing += canvasWrapper.Converter.XOffset;
            for (int i = 0; i < canvasWrapper.GridLines; i++) {
                var labelValue = canvasWrapper.Converter
                                          .XValueToRealScale(widthSpacing, minMax.Xmax, minMax.Xmin);
                _labelPaint.TextSize = canvasWrapper.LabelTextSize;
                if (canvasWrapper.ThisIsiOSOrAndroid) {
                    axis.DrawAndPositionXTickMark(GetXLabel(labelValue), widthSpacing, canvasWrapper.ChartArea.Bottom, _labelPaint);
                }
                else {
                    axis.DrawAndPositionXTickMark(GetXLabel(labelValue), widthSpacing, (canvasWrapper.Converter.YOffset * 2) + 4f, _labelPaint);
                }
                widthSpacing += widthHolder;
            }
        }

        /// <summary>
        /// Gets and sets the name of the series. Note that this is not rendered for single value charts.
        /// The label property is rendered instead.
        /// </summary>
        public string ChartName { get; set; } = "Chart Name";

        /// <summary>
        /// Pixel scale data used for rendering the lines. Generated from X-Y values
        /// </summary>
        public List<SKPoint> ConstructionData { get; internal set; }

        /// <summary>
        /// Points generated from the original X-Y values
        /// </summary>
        public IEnumerable<SKPoint> OriginalData { get; set; }

        /// <summary>
        /// A collection of Labels used for the X-Axis
        /// </summary>
        public List<string> XLabel { get; private set; }

        /// <summary>
        /// A collection of Labels used for the Y-Axis
        /// </summary>
        public List<string> YLabel { get; private set; }

        /// <summary>
        /// The data type of the XValues
        /// </summary>
        public Type XValueType { get; private set; }

        /// <summary>
        /// The data type of the YValues
        /// </summary>
        public Type YValueType { get; private set; }

        private SKColor _chartColor;
        /// <summary>
        /// The color of the line to be ploted
        /// </summary>
        public SKColor ChartColor {
            get => _chartColor;
            set {
                if (value != _chartColor) {
                    _chartColor = value;
                    _chartPaint.Color = value;
                }
            }
        }

        private SKColor _labelColor;
        /// <summary>
        ///  Color of Label
        /// </summary>
        public SKColor LabelColor {
            get => _labelColor;
            set {
                if (value != _labelColor) {
                    _labelColor = value;
                    _labelPaint.Color = value;
                }
            }
        }

        //The SkPaint used for drawing charts
        protected readonly SKPaint _chartPaint = new SKPaint() {
            IsAntialias = true,
            Style = SKPaintStyle.StrokeAndFill,
            Color = SKColors.Green,
            StrokeCap = SKStrokeCap.Round,
            TextSize = 20f
        };

        private float _labelTextSize = 20f;
        /// <summary>
        /// The TextSize of labels
        /// </summary>
        public float LabelTextSize {
            get => _labelTextSize;
            set {
                if (value != _labelTextSize) {
                    _labelTextSize = value;
                    _labelPaint.TextSize = value;
                }
            }
        }

        //The SkPaint used for drawing labels
        protected readonly SKPaint _labelPaint = new SKPaint() {
            Style = SKPaintStyle.StrokeAndFill,
            IsAntialias = true,
            Color = SKColors.Black,
            TextSize = 20f
        };

        public abstract void RenderChart(CanvasWrapper canvas, Axis axis, IMinMax minMax);
    }
}
