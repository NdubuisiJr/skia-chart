using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkiaChart.Charts {

    /// <summary>
    /// A class that handles the plotting of a single line chart on the ChartCanvas
    /// </summary>
    public class LineChart : IChart {
        
        /// <summary>
        /// Instantiates an instance of LineChart with floating point values
        /// </summary>
        /// <param name="xValues">X-Cordinates of the X-Y plot</param>
        /// <param name="yValues">Y-Cordinates of the X-Y plot</param>
        public LineChart(IEnumerable<float> xValues, IEnumerable<float> yValues) {
            ValidateInputs(xValues, yValues);
            UpdateDateType<float, float>();
            OriginalData = GenerateXYPoints(xValues, yValues);
        }

        /// <summary>
        /// Instantiates an instance of LineChart with X-Values as labels and YValues as floats
        /// </summary>
        /// <param name="xValues">X-Cordinates of the X-Y plot as string labels</param>
        /// <param name="yValues">Y-Cordinates of the X-Y plot</param>
        public LineChart(IEnumerable<string> xValues, IEnumerable<float> yValues) {
            ValidateInputs(xValues, yValues);
            UpdateDateType<string, float>();
            OriginalData = DistributeXGenerateYPoints(xValues, yValues);
        }

        /// <summary>
        /// Instantiates an instance of LineChart with Y-Values as labels and XValues as floats
        /// </summary>
        /// <param name="xValues">X-Cordinates of the X-Y plot</param>
        /// <param name="yValues">Y-Cordinates of the X-Y plot as string labels</param>
        public LineChart(IEnumerable<float> xValues, IEnumerable<string> yValues) {
            ValidateInputs(xValues, yValues);
            UpdateDateType<float, string>();
            OriginalData = DistributeYGenerateXPoints(xValues, yValues);
        }

        /// <summary>
        /// Renders a line chart on a given SKCanvas. It is preferable to allow the
        /// chart class to call this method.
        /// </summary>
        /// <param name="canvas">Canvas to draw the line chart</param>
        public void RenderChart(SKCanvas canvas) {
            var firstPoint = ConstructionData.ElementAt(0);
            var chartPath = new SKPath();
            chartPath.MoveTo(firstPoint);
            for (int index = 1; index < ConstructionData.Count(); index++) {
                chartPath.LineTo(ConstructionData.ElementAt(index));
            }
            canvas.DrawPath(chartPath, _chartPaint);
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
                return labelValue.ToString();
            }

            var index = int.Parse(Math.Round(labelValue, 0).ToString());
            return YLabel[index];
        }

        //Generates points for drawing (graphics points) given the X-Y values
        private IEnumerable<SKPoint> GenerateXYPoints(IEnumerable<float> xValues, 
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
        private IEnumerable<SKPoint> DistributeXGenerateYPoints(IEnumerable<string> xValues, 
            IEnumerable<float> yValues) {
            var numberOfPoints = xValues.Count();
            XLabel = new List<string>();
            var points = new List<SKPoint>();
            for (int i = 0; i < numberOfPoints; i++) {
                points.Add(new SKPoint(i, yValues.ElementAt(i)));
                XLabel.Add(xValues.ElementAt(i));
            }
            return points;
        }

        //Generates points for drawing (graphics points) by distributing Y from 0 to count
        //and X for give YValues
        private IEnumerable<SKPoint> DistributeYGenerateXPoints(
            IEnumerable<float> xValues, IEnumerable<string> yValues) {
            var numberOfPoints = xValues.Count();
            YLabel = new List<string>();
            var points = new List<SKPoint>();
            for (int i = 0; i < numberOfPoints; i++) {
                points.Add(new SKPoint(xValues.ElementAt(i),i));
                YLabel.Add(yValues.ElementAt(i));
            }
            return points;
        }

        //Keeps track of the X-Y data types
        private void UpdateDateType<TxValues, TyValues>() {
            XValueType = typeof(TxValues);
            YValueType = typeof(TyValues);
        }

        // Validates the inputed X-Y values
        private static void ValidateInputs<TxValues, TyValues>(
            IEnumerable<TxValues> xValues, IEnumerable<TyValues> yValues) {
            if (xValues == null || yValues == null) {
                throw new ArgumentNullException("xValues and yValues must not be null");
            }

            if (xValues.Count() != yValues.Count()) {
                throw new ArgumentException("Number of xValues must equal number of yValues");
            }
        }

        /// <summary>
        /// Pixel scale data used for rendering the lines. Generated from X-Y values
        /// </summary>
        public IEnumerable<SKPoint> ConstructionData { get; set; }

        /// <summary>
        /// Points generated from the original X-Y values
        /// </summary>
        public IEnumerable<SKPoint> OriginalData { get; set; }

        /// <summary>
        /// A collection of Labels used for the X-Axis
        /// </summary>
        public List<string> XLabel { get; set; }

        /// <summary>
        /// A collection of Labels used for the Y-Axis
        /// </summary>
        public List<string> YLabel { get; set; }

        /// <summary>
        /// The data type of the XValues
        /// </summary>
        public Type XValueType { get; set; }

        /// <summary>
        /// The data type of the YValues
        /// </summary>
        public Type YValueType { get; set; }

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

        private float _strokeWidth;
        /// <summary>
        /// The stroke with of the line chart
        /// </summary>
        public float StrokeWidth {
            get=> _strokeWidth;
            set{
                if (value != _strokeWidth) {
                    _strokeWidth = value;
                    _chartPaint.StrokeWidth = value;
                }
            }
        } 

        //The SkPaint used for drawing the line chart
        private readonly SKPaint _chartPaint = new SKPaint() {
            IsAntialias = true,
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Green,
            StrokeWidth=5,
            StrokeCap = SKStrokeCap.Round
        };
    }
}
