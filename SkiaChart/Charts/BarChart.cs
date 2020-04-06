using SkiaChart.Axes;
using SkiaChart.Interfaces;
using SkiaChart.Models;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;

namespace SkiaChart.Charts {
    /// <summary>
    /// Handles the plotting of bar chart on the Chart Canvas
    /// </summary>
    public class BarChart : ChartBase {
        /// <summary>
        /// Creates an of the Barchart with the X-Axis labels and Y-Axis values
        /// </summary>
        /// <param name="xLabels">Labels used on the X-Axis</param>
        /// <param name="yValues">Values used to construct the Y-Axis</param>
        public BarChart(IEnumerable<string> xLabels, IEnumerable<float> yValues) {
            ValidateInputs(xLabels, yValues);
            UpdateDateType<string, float>();
            OriginalData = DistributeXGenerateYPoints(xLabels, yValues);
        }

        /// <summary>
        /// Renders a bar chart on the canvas. This method is called by the Chart class.
        /// </summary>
        /// <param name="canvasWrapper">wrapper class containing info mation about the canvas and chart</param>
        /// <param name="axis">Axis orientation object</param>
        /// <param name="minMax">Data for the extreme values</param>
        /// <param name="gridPaint">Paint object for the grid lines</param>
        public override void RenderChart(CanvasWrapper canvasWrapper, Axis axis, IMinMax minMax) {
            CheckConstructionPolicy(nameof(BarChart));

            var drawingSpace = canvasWrapper.ChartArea.Right / ConstructionData.Count() * 0.9;
            var rectSpace = (float)drawingSpace / canvasWrapper.NumberOfCharts;
            var columnSpace = (float)((canvasWrapper.ChartArea.Right / ConstructionData.Count()) - drawingSpace) / 2;
            var pointX = canvasWrapper.ChartArea.Width / ConstructionData.Count();
            var counter = 0;
            foreach (var point in ConstructionData) {
                var x1 = (pointX * counter) + canvasWrapper.ChartArea.Left + columnSpace +
                    (canvasWrapper.NumberPlottedChart * rectSpace);
                var x2 = x1 + rectSpace - columnSpace;
                var rect = new SKRect(x1, point.Y, x2, canvasWrapper.ChartArea.Top);
                _chartPaint.IsStroke = IsStroked;
                canvasWrapper.Canvas.DrawRect(rect, _chartPaint);

                var xLabel = XLabel[counter];
                axis.DrawAndPositionXTickMark(xLabel, (x1 + (x2 - x1) / 2), 
                    canvasWrapper.ChartArea.Bottom, _labelPaint);

                var yLabel = GetYLabel(OriginalData.ElementAt(counter).Y);
                axis.DrawAndPositionYTickMark(yLabel, point.Y, (x1 + (x2 - x1) / 2), _labelPaint);
                counter++;
            }
            canvasWrapper.NumberPlottedChart += 1;
        }

        public bool IsStroked { get; set; } = false;

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

        private readonly SKPaint _labelPaint = new SKPaint() {
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            StrokeWidth = 3,
            Color = SKColors.Gray
        };
    }
}
