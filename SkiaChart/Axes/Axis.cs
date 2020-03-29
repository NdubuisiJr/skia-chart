using SkiaChart.Charts;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;

namespace SkiaChart.Axes {
    /// <summary>
    /// The abstract class that cordinates the Axis of the chart
    /// </summary>
    public abstract class Axis {
        internal Axis(IEnumerable<ChartBase> lineSeries) {
            LineSeries = lineSeries;
        }

        internal float FindXMax() {
            var xMax = float.MinValue;
            foreach (var serie in LineSeries) {
                var xData = serie.OriginalData.Select(x => x.X).ToList();
                foreach (var xValue in xData) {
                    if (xValue > xMax) {
                        xMax = xValue;
                    }
                }
            }
            return xMax;
        }

        internal float FindXMin() {
            var xMin = float.MaxValue;
            foreach (var serie in LineSeries) {
                var xData = serie.OriginalData.Select(x => x.X).ToList();
                foreach (var xValue in xData) {
                    if (xValue < xMin) {
                        xMin = xValue;
                    }
                }
            }
            return xMin;
        }

        internal float FindYMax() {
            var yMax = float.MinValue;
            foreach (var serie in LineSeries) {
                var yData = serie.OriginalData.Select(y => y.Y);
                foreach (var yValue in yData) {
                    if (yValue > yMax) {
                        yMax = yValue;
                    }
                }
            }
            return yMax;
        }

        internal float FindYMin() {
            var yMin = float.MaxValue;
            foreach (var serie in LineSeries) {
                var yData = serie.OriginalData.Select(y => y.Y);
                foreach (var yValue in yData) {
                    if (yValue < yMin) {
                        yMin = yValue;
                    }
                }
            }
            return yMin;
        }

        internal abstract void OrientAxis(SKCanvas canvas, float width, 
            float height);

        internal abstract void PositionXLabel(string label, float widthSpacing, 
            float bottomOrTop, SKPaint paint);

        internal abstract void PositionYLabel(string label, float heightSpacing, 
            float rightOrLeft, SKPaint paint);

        internal IEnumerable<ChartBase> LineSeries;

    }
}
