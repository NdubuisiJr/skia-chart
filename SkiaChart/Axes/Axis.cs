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

        internal virtual void AntiOrientAxis(float heightSpacing) { }

        internal abstract void OrientAxis(SKCanvas canvas, float deviceWidth, 
            float deviceHeight, float xOffset, float yOffset);

        internal abstract void DrawAndPositionXTickMark(string label, float widthSpacing, 
            float bottomOrTop, SKPaint paint);

        internal abstract void DrawAndPositionYTickMark(string label, float heightSpacing, 
            float rightOrLeft, SKPaint paint);

        internal abstract void DrawAndPositionLegend(string legend, float heightSpacing,
            float basePosition, SKPaint paint, bool isFirstCall = false);

        internal abstract void DrawAndPositionXLabel(string label, float bottomOrTop, SKPaint paint);

        internal abstract void DrawAndPositionYLabel(string label, float rightOrLeft, SKPaint paint);

        internal IEnumerable<ChartBase> LineSeries;
    }
}
