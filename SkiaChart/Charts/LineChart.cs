using SkiaChart.Models;
using SkiaSharp;
using System;
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
        public override void RenderChart(CanvasWrapper canvasWrapper) {
            CheckConstructionPolicy(nameof(LineChart));

            var canvas = canvasWrapper.Canvas;
            var firstPoint = ConstructionData.ElementAt(0);
            var chartPath = new SKPath();
            chartPath.MoveTo(firstPoint);
            for (int index = 1; index < ConstructionData.Count(); index++) {
                chartPath.LineTo(ConstructionData.ElementAt(index));
            }
            canvas.DrawPath(chartPath, _chartPaint);
        }
    }
}
