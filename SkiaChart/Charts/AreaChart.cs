using SkiaChart.Axes;
using SkiaChart.Enums;
using SkiaChart.Helpers;
using SkiaChart.Interfaces;
using SkiaChart.Models;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;

namespace SkiaChart.Charts
{
    public class AreaChart : LineChart
    {
        /// <summary>
        /// Instantiates an instance of Area with floating point values
        /// </summary>
        /// <param name="xValues">X-Cordinates of the X-Y plot</param>
        /// <param name="yValues">Y-Cordinates of the X-Y plot</param>
        public AreaChart(IEnumerable<float> xValues, IEnumerable<float> yValues)
            : base(xValues, yValues)
        {
        }

        /// <summary>
        /// Instantiates an instance of Area with X-Values as labels and YValues as floats
        /// </summary>
        /// <param name="xValues">X-Cordinates of the X-Y plot as string labels</param>
        /// <param name="yValues">Y-Cordinates of the X-Y plot</param>
        public AreaChart(IEnumerable<string> xValues, IEnumerable<float> yValues)
             : base(xValues, yValues)
        {
        }

        /// <summary>
        /// Instantiates an instance of Area with Y-Values as labels and XValues as floats
        /// </summary>
        /// <param name="xValues">X-Cordinates of the X-Y plot</param>
        /// <param name="yValues">Y-Cordinates of the X-Y plot as string labels</param>
        public AreaChart(IEnumerable<float> xValues, IEnumerable<string> yValues)
             : base(xValues, yValues)
        {
        }

        /// <summary>
        /// Renders an Area chart a canvas. This method is called by the chart class.
        /// </summary>
        /// <param name="canvasWrapper">wrapper class containing info mation about the canvas and chart</param>
        /// <param name="axis">Axis orientation object</param>
        /// <param name="minMax">Data for the extreme values</param>
        /// <param name="gridPaint">Paint object for the grid lines</param>
        public override void RenderChart(CanvasWrapper canvasWrapper, Axis axis, IMinMax minMax)
        {
            CheckConstructionPolicy(nameof(LineChart));

            if (canvasWrapper.NumberPlottedChart < 1)
            {
                DrawHorizontalLabels(canvasWrapper, axis, minMax);
                DrawVerticalLabels(canvasWrapper, axis, minMax);
            }

            var canvas = canvasWrapper.Canvas;
            var path = SplineHelper.CreateCatmullRomSpline(ConstructionData, closed: true);

            if (ShowPoints)
            {
                foreach (var point in ConstructionData.Skip(1))
                {
                    canvas.DrawCircle(point, PointRadius, _chartPaint);
                }
            }

            path.FillType = SKPathFillType.EvenOdd;
            path.Close();
            _chartPaint.IsStroke = false;
            canvas.DrawPath(path, _chartPaint);
            canvasWrapper.NumberPlottedChart += 1;

            if (canvasWrapper.CanShowLegend)
            {
                RenderLegend(canvasWrapper, axis, canvas, PointPlotVariant.AreaChart);
            }
        }
    }
}
