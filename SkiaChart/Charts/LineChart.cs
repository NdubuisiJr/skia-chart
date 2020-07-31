using SkiaChart.Axes;
using SkiaChart.Enums;
using SkiaChart.Interfaces;
using SkiaChart.Models;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;

namespace SkiaChart.Charts {

    /// <summary>
    /// A class that handles the plotting of a single line chart on the ChartCanvas
    /// </summary>
    public class LineChart : ChartBase {

        /// <summary>
        /// Instantiates an instance of LineChart with floating point values
        /// </summary>
        /// <param name="xValues">X-Cordinates of the X-Y plot</param>
        /// <param name="yValues">Y-Cordinates of the X-Y plot</param>
        public LineChart(IEnumerable<float> xValues, IEnumerable<float> yValues) {
            ValidateInputs(xValues, yValues);
            UpdateDataType<float, float>();
            OriginalData = GenerateXYPoints(xValues, yValues);
        }

        /// <summary>
        /// Instantiates an instance of LineChart with X-Values as labels and YValues as floats
        /// </summary>
        /// <param name="xValues">X-Cordinates of the X-Y plot as string labels</param>
        /// <param name="yValues">Y-Cordinates of the X-Y plot</param>
        public LineChart(IEnumerable<string> xValues, IEnumerable<float> yValues) {
            ValidateInputs(xValues, yValues);
            UpdateDataType<string, float>();
            OriginalData = DistributeXGenerateYPoints(xValues, yValues);
        }

        /// <summary>
        /// Instantiates an instance of LineChart with Y-Values as labels and XValues as floats
        /// </summary>
        /// <param name="xValues">X-Cordinates of the X-Y plot</param>
        /// <param name="yValues">Y-Cordinates of the X-Y plot as string labels</param>
        public LineChart(IEnumerable<float> xValues, IEnumerable<string> yValues) {
            ValidateInputs(xValues, yValues);
            UpdateDataType<float, string>();
            OriginalData = DistributeYGenerateXPoints(xValues, yValues);
        }

        /// <summary>
        /// Renders a line chart a canvas. This method is called by the chart class.
        /// </summary>
        /// <param name="canvasWrapper">wrapper class containing info mation about the canvas and chart</param>
        /// <param name="axis">Axis orientation object</param>
        /// <param name="minMax">Data for the extreme values</param>
        /// <param name="gridPaint">Paint object for the grid lines</param>
        public override void RenderChart(CanvasWrapper canvasWrapper, Axis axis, IMinMax minMax) {
            CheckConstructionPolicy(nameof(LineChart));
            var canvas = canvasWrapper.Canvas;

            if (canvasWrapper.NumberPlottedChart < 1) {
                DrawHorizontalLabels(canvasWrapper, axis, minMax);
                DrawVerticalLabels(canvasWrapper, axis, minMax);
            }

            _chartPaint.IsStroke = true;
            var path = new SKPath();
            path.MoveTo(ConstructionData.First());
            foreach (var point in ConstructionData.Skip(1)) {
                path.LineTo(point);
                if(ShowPoints)
                    canvas.DrawCircle(point, PointRadius, _chartPaint);
            }
            canvas.DrawPath(path, _chartPaint);
            canvasWrapper.NumberPlottedChart += 1;

            if (canvasWrapper.CanShowLegend) {
                RenderLegend(canvasWrapper, axis, canvas,PointPlotVariant.LineChart);
            }
        }

        /// <summary>
        /// Sets and gets a value indicating whether point markers should be showed
        /// </summary>
        public bool ShowPoints { get; set; } = false;

        /// <summary>
        /// Radius of the scatter points in pixels
        /// </summary>
		public float PointRadius { get; set; } = 1;

        /// <summary>
        /// Makes the points hollow. It is false by default.
        /// </summary>
        public bool IsStroked { get; set; } = false;

        private float _strokeWidth;
        /// <summary>
        /// The stroke with of the line chart
        /// </summary>
        public float Width {
            get => _strokeWidth;
            set {
                if (value != _strokeWidth) {
                    _strokeWidth = value;
                    _chartPaint.StrokeWidth = value;
                }
            }
        }
    }
}
