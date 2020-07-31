using SkiaChart.Axes;
using SkiaChart.Enums;
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
            UpdateDataType<string, float>();
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

            var count = ConstructionData.Count();
            var drawingSpace = canvasWrapper.ChartArea.Right / count * 0.9;
            var rectSpace = (float)drawingSpace / canvasWrapper.NumberOfCharts;
            var columnSpace = (float)((canvasWrapper.ChartArea.Right / count) - drawingSpace) / 2;
            var pointX = canvasWrapper.ChartArea.Width / count;
            var counter = 0;
            foreach (var point in ConstructionData) {
                var x1 = (pointX * counter) + canvasWrapper.ChartArea.Left + columnSpace +
                    (canvasWrapper.NumberPlottedChart * rectSpace);
                var x2 = x1 + rectSpace - columnSpace;
                var rect = new SKRect(x1, point.Y, x2, canvasWrapper.ChartArea.Top);
                _chartPaint.IsStroke = IsStroked;
                canvasWrapper.Canvas.DrawRect(rect, _chartPaint);

                var xLabel = XLabel[counter];
                _labelPaint.TextSize = canvasWrapper.LabelTextSize;
                if (canvasWrapper.NumberPlottedChart == 0) {
                    RenderXTickMark(canvasWrapper, axis, x1, x2, xLabel);
                    var yLabel = GetYLabel(OriginalData.ElementAt(counter).Y);
                    if (yLabel.Trim().Length > 5) {
                        canvasWrapper.drawYTickMarkOnBars = false;
                        DrawVerticalLabels(canvasWrapper, axis, minMax);
                    }
                }
                if (canvasWrapper.drawYTickMarkOnBars)
                    RenderYTickMark(canvasWrapper, axis, counter, point, rect);
                counter++;
            }
            canvasWrapper.NumberPlottedChart += 1;
            if (canvasWrapper.CanShowLegend) {
                RenderLegend(canvasWrapper, axis, canvasWrapper.Canvas, PointPlotVariant.AreaChart);
            }
        }

        private void RenderYTickMark(CanvasWrapper canvasWrapper, Axis axis, int counter, SKPoint point, SKRect rect) {
            var yLabel = GetYLabel(OriginalData.ElementAt(counter).Y);
            _labelPaint.TextSize = canvasWrapper.LabelTextSize;
            axis.DrawAndPositionYTickMark(yLabel, point.Y, rect.Left, _labelPaint);
        }

        private void RenderXTickMark(CanvasWrapper canvasWrapper, Axis axis, float x1, float x2, string xLabel) {
            var widthSpace = canvasWrapper.NumberOfCharts == 1 ? (x1 + (x2 - x1) / 2) : (x1 + (x2 - x1));
            if (canvasWrapper.ThisIsiOSOrAndroid) {
                axis.DrawAndPositionXTickMark(xLabel, widthSpace,
                    canvasWrapper.ChartArea.Bottom, _labelPaint);
            }
            else {
                axis.DrawAndPositionXTickMark(xLabel, widthSpace,
                    (canvasWrapper.Converter.YOffset * 2) + 4f, _labelPaint);
            }
        }

        public bool IsStroked { get; set; } = false;
    }
}
