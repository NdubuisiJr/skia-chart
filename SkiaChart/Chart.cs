using SkiaChart.Axes;
using SkiaChart.Charts;
using SkiaChart.Exceptions;
using SkiaChart.Helpers;
using SkiaChart.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkiaChart {
    /// <summary>
    /// The chart class that cordinates all types of chart display on the canvas.
    /// </summary>
    /// <typeparam name="T">Type of chart to display</typeparam>
    public class Chart<T> where T : ChartBase {

        /// <summary>
        /// Creates an instance of the chart class with a collection of charts to display
        /// </summary>
        /// <param name="charts">The collection of a given type to plot</param>
        public Chart(IEnumerable<T> charts, Axis<T> axis = null) {
            ValidateCharts(charts);
            _charts = new List<T>(charts);
            _labelChart = charts.ElementAt(0);
            SetAxis(charts, axis);
            ScaleAxes();
        }

        //Initiates the rendering of all the charts in the collection
        internal void Plot(CanvasWrapper canvasWrapper) {
            NormalizeAllDataPoints();
            canvasWrapper.NumberOfCharts = _charts.Count;
            _charts.ForEach(chart => chart.RenderChart(canvasWrapper));
        }

        //Sets the grid and Initiates the drawing of the grid lines
        internal void SetGrid(SKCanvas canvas, int gridLines) {
            _converter = new Converter(ChartArea, XOffset, YOffset);
            var widthSpacing = (ChartArea.Right - ChartArea.Left) / gridLines;
            var heightSpacing = (ChartArea.Bottom - ChartArea.Top) / gridLines;
            ConstructVerticalLines(canvas, gridLines, widthSpacing);
            ConstructHorizontalLines(canvas, gridLines, heightSpacing);
        }

        //Initiates the conversion chart points
        private void NormalizeAllDataPoints() {
            _charts.ForEach(serie => {
                serie.ConstructionData = NormalizePoints(serie.OriginalData);
            });
        }

        //draws horizontal grid lines on the given canvas
        private void ConstructHorizontalLines(SKCanvas canvas, int gridLines, float heightSpacing) {
            var left = ChartArea.Left;
            var right = ChartArea.Right;
            var heightHolder = heightSpacing;
            heightSpacing += YOffset;
            canvas.DrawLine(new SKPoint(left, YOffset), new SKPoint(right, YOffset), _gridPaint);
            for (int i = 0; i < gridLines; i++) {
                canvas.DrawLine(new SKPoint(left, heightSpacing), new SKPoint(right, heightSpacing), _gridPaint);
                var labelValue = _converter.YValueToRealScale(heightSpacing, Ymax, Ymin);
                Axis.PositionYLabel(_labelChart.GetYLabel(labelValue), heightSpacing, left, _gridPaint);
                heightSpacing += heightHolder;
            }
        }

        //draws vertical grid lines on the given canvas
        private void ConstructVerticalLines(SKCanvas canvas, int gridLines, float widthSpacing) {
            var top = ChartArea.Top;
            var bottom = ChartArea.Bottom;
            var widthHolder = widthSpacing;
            widthSpacing += XOffset;
            canvas.DrawLine(new SKPoint(XOffset, top), new SKPoint(XOffset, bottom), _gridPaint);
            for (int i = 0; i < gridLines; i++) {
                canvas.DrawLine(new SKPoint(widthSpacing, top), new SKPoint(widthSpacing, bottom), _gridPaint);
                var labelValue = _converter.XValueToRealScale(widthSpacing, Xmax, Xmin);
                Axis.PositionXLabel(_labelChart.GetXLabel(labelValue), widthSpacing, bottom, _gridPaint);
                widthSpacing += widthHolder;
            }
            widthSpacing = (ChartArea.Right - ChartArea.Left) / 2;
            Axis.PositionXLabel("Time", widthSpacing, bottom + 40, _gridPaint);//This is a quick fix (Remove)
        }

        //Initiates the conversion from real world data to pixel scale data
        private List<SKPoint> NormalizePoints(IEnumerable<SKPoint> chartValues) {
            var listOfPoints = new List<SKPoint>();
            foreach (var point in chartValues) {
                listOfPoints.Add(_converter.ToPixelScale(point, Xmax, Xmin, Ymax, Ymin));
            }
            return listOfPoints;
        }

        //Automatical scales the axes with the max and min values from the data
        private void ScaleAxes() {
            var maxValue = float.MaxValue;
            if (Xmax == maxValue) {
                Xmax = Axis.FindXMax();
            }
            if (Xmin == maxValue) {
                Xmin = Axis.FindXMin();
            }
            if (Ymax == maxValue) {
                Ymax = Axis.FindYMax();
            }
            if (Ymin == maxValue) {
                Ymin = Axis.FindYMin();
            }
        }

        //Validates chart inputs
        private void ValidateCharts(IEnumerable<T> charts) {
            if (charts == null || charts.Count() < 1) {
                throw new ArgumentException($"{nameof(charts)}");
            }
            else {
                var numberOfCharts = charts.Count();
                for (int index = 0; index < numberOfCharts - 1; index++) {
                    var check1 = charts.ElementAt(index).XValueType != charts.ElementAt(index + 1).XValueType;
                    var check2 = charts.ElementAt(index).YValueType != charts.ElementAt(index + 1).YValueType;
                    if (check1 || check2) {
                        var message = "The data type of the values provided for the " +
                            "x-coordinates or the y-coordinates is not consistent";
                        throw new InconsistentChartValueTypeException(message);
                    }
                }
            }
        }

        //Setup chart Orientation
        private void SetAxis(IEnumerable<T> charts, Axis<T> axis) {
            if (axis == null) {
                Axis = new NormalXYAxis<T>(charts);
            }
            else {
                Axis = axis;
            }
        }

        /// <summary>
        /// Gets and sets the Offset from the left of the screen
        /// </summary>
        public float XOffset { get; set; }

        /// <summary>
        /// Gets and sets the Offset from the left of the screen
        /// </summary>
        public float YOffset { get; set; }

        /// <summary>
        /// Gets and sets the minimum value in the X-Axis
        /// </summary>
        public float Xmin { get; set; } = float.MaxValue;

        /// <summary>
        ///Gets and sets the minimum value in the Y-Axis 
        /// </summary>
        public float Ymin { get; set; } = float.MaxValue;

        /// <summary>
        /// Gets and sets the maximum value in the X-Axis
        /// </summary>
        public float Xmax { get; set; } = float.MaxValue;

        /// <summary>
        /// Gets and sets the maximum value in the Y-Axis
        /// </summary>
        public float Ymax { get; set; } = float.MaxValue;

        /// <summary>
        /// Gets and sets the axis orientation for the chart
        /// </summary>
        public Axis<T> Axis { get; set; }
        internal SKRect ChartArea { get; set; }
        private SKColor _gridColor;
        internal SKColor GridColor {
            get => _gridColor;
            set {
                if (value != _gridColor) {
                    _gridColor = value;
                    _gridPaint.Color = value;
                }
            }
        }

        private Converter _converter;
        private readonly List<T> _charts;
        private readonly T _labelChart;
        private readonly SKPaint _gridPaint = new SKPaint() {
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            StrokeWidth = 3,
            Color = SKColors.Gray
        };
    }
}
