using SkiaChart.Axes;
using SkiaChart.Enums;
using SkiaChart.Interfaces;
using SkiaChart.Models;
using SkiaSharp;
using System.Collections.Generic;

namespace SkiaChart.Charts {
    public class ScatterChart : LineChart {

        /// <summary>
        /// Instantiates an instance of Scatter with floating point values
        /// </summary>
        /// <param name="xValues">X-Cordinates of the X-Y plot</param>
        /// <param name="yValues">Y-Cordinates of the X-Y plot</param>
        public ScatterChart(IEnumerable<float> xValues, IEnumerable<float> yValues)
            : base(xValues, yValues) {
            SetScatterChartDefaults();
        }

        /// <summary>
        /// Instantiates an instance of Scatter with X-Values as labels and YValues as floats
        /// </summary>
        /// <param name="xValues">X-Cordinates of the X-Y plot as string labels</param>
        /// <param name="yValues">Y-Cordinates of the X-Y plot</param>
        public ScatterChart(IEnumerable<string> xValues, IEnumerable<float> yValues)
             : base(xValues, yValues) {
            SetScatterChartDefaults();
        }

        /// <summary>
        /// Instantiates an instance of Scatter with Y-Values as labels and XValues as floats
        /// </summary>
        /// <param name="xValues">X-Cordinates of the X-Y plot</param>
        /// <param name="yValues">Y-Cordinates of the X-Y plot as string labels</param>
        public ScatterChart(IEnumerable<float> xValues, IEnumerable<string> yValues)
             : base(xValues, yValues) {
            SetScatterChartDefaults();
        }

        /// <summary>
        /// Renders a Scatter chart a canvas. This method is called by the chart class.
        /// </summary>
        /// <param name="canvasWrapper">wrapper class containing info mation about the canvas and chart</param>
        /// <param name="axis">Axis orientation object</param>
        /// <param name="minMax">Data for the extreme values</param>
        /// <param name="gridPaint">Paint object for the grid lines</param>
        public override void RenderChart(CanvasWrapper canvasWrapper, Axis axis, IMinMax minMax) {
            CheckConstructionPolicy(nameof(ScatterChart));

            if (canvasWrapper.NumberPlottedChart < 1) {
                DrawHorizontalLabels(canvasWrapper, axis, minMax);
                DrawVerticalLabels(canvasWrapper, axis, minMax);
            }

            var canvas = DisplayPoints(canvasWrapper);
            canvasWrapper.NumberPlottedChart += 1;

            if (canvasWrapper.CanShowLegend) {
                RenderLegend(canvasWrapper, axis, canvas, PointPlotVariant.ScatterChart);
            }
        }

        //Draw points
        protected SKCanvas DisplayPoints(CanvasWrapper canvasWrapper) {
            var canvas = canvasWrapper.Canvas;
            _chartPaint.IsStroke = IsStroked;
            foreach (var point in ConstructionData) {
                canvas.DrawCircle(point, PointRadius, _chartPaint);
            }
            return canvas;
        }

        private void SetScatterChartDefaults() {
            PointRadius = 5;
            ShowPoints = true;
        }
    }
}
