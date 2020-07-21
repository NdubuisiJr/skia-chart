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
                var xData = serie.OriginalData.Select(x => x.X);
                var maxXData = xData.Max();
                if (maxXData > xMax) {
                    xMax = maxXData;
                }
            }
            return xMax;
        }

        internal float FindXMin() {
            var xMin = float.MaxValue;
            foreach (var serie in LineSeries) {
                var xData = serie.OriginalData.Select(x => x.X);
                var xMinData = xData.Min();
                if (xMinData < xMin) {
                    xMin = xMinData;
                }
            }
            return xMin;
        }

        internal float FindYMax() {
            var yMax = float.MinValue;
            foreach (var serie in LineSeries) {
                var yData = serie.OriginalData.Select(y => y.Y);
                var yMaxData = yData.Max();
                if (yMaxData > yMax) {
                    yMax = yMaxData;
                }
            }
            return yMax;
        }

        internal float FindYMin() {
            var yMin = float.MaxValue;
            foreach (var serie in LineSeries) {
                var yData = serie.OriginalData.Select(y => y.Y);
                var yMinData = yData.Min();
                if (yMinData < yMin) {
                    yMin = yMinData;
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
            float basePosition, SKPaint paint, float LegendItemSpacing = 40f, bool isFirstCall = false);

        internal abstract void DrawAndPositionXLabel(string label, float bottomOrTop, SKPaint paint);

        internal abstract void DrawAndPositionYLabel(string label, float rightOrLeft, SKPaint paint);

        internal IEnumerable<ChartBase> LineSeries;
    }
}
