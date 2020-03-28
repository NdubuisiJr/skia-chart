using SkiaChart.Models;
using SkiaSharp;
using System;
using System.Collections;
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

        public override void RenderChart(CanvasWrapper canvasWrapper) {
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
                counter++;
            }
            canvasWrapper.NumberPlottedChart += 1;
        }
        public bool IsStroked { get; set; } = false;
    }
}
