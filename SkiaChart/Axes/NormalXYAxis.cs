using System.Collections.Generic;
using SkiaChart.Charts;
using SkiaSharp;

namespace SkiaChart.Axes {
    /// <summary>
    /// A class that orients the X-Y axis of a chart to the bottom left hand corner
    /// </summary>
    /// <typeparam name="T">type of chart</typeparam>
    public class NormalXYAxis : Axis {
        public NormalXYAxis(IEnumerable<ChartBase> lineSeries) : base(lineSeries) {
        }

        internal override void OrientAxis(SKCanvas canvas, float width,
            float height, float xOffset, float yOffset) {
            _deviceHeight = height;
            _deviceWidth = width;
            _canvas = canvas;
            _xOffset = xOffset;
            _yOffset = yOffset;

            canvas.Translate(0, height);
            canvas.Scale(1, -1);
        }

        private void Execute(string text, float spacing, float basePosition, SKPaint paint,
            bool isHorizontal) {
            _canvas.Save();
            if (isHorizontal) {
                AntiOrientAxis(float.MaxValue);
                _canvas.DrawText(text, spacing, basePosition, paint);
            }
            else {
                AntiOrientAxis(spacing);
                _canvas.DrawText(text, basePosition, 0, paint);
            }
            _canvas.Restore();
        }

        internal override void DrawAndPositionXTickMark(string label, float widthSpacing,
            float bottomOrTop, SKPaint paint) {
            Execute(label, widthSpacing - (label.Length / 2f), bottomOrTop + 20, paint, true);
        }

        internal override void DrawAndPositionYTickMark(string label, float heightSpacing,
            float rightOrLeft, SKPaint paint) {
            Execute(label, heightSpacing, rightOrLeft - 20, paint, false);
        }

        internal override void DrawAndPositionLegend(string legend, float heightSpacing, float basePosition,
            SKPaint paint, bool isFirstCall = false) {
            if (isFirstCall) {
                _canvas.Save();
                AntiOrientAxis(float.MaxValue);
                var height = (int.Parse(legend) + 1) * 40f;
                var rectangle = new SKRect(basePosition, heightSpacing + 60, _deviceWidth - _xOffset, heightSpacing + 60 + height);
                _canvas.DrawRect(rectangle, paint);
                _canvas.Restore();
            }
            else {
                Execute(legend, basePosition, heightSpacing, paint, true);
            }

        }

        internal override void DrawAndPositionXLabel(string label, float bottomOrTop, SKPaint paint) {
            Execute(label, (_deviceWidth / 2) - (label.Length), bottomOrTop + 45, paint, true);
        }

        internal override void DrawAndPositionYLabel(string label, float rightOrLeft, SKPaint paint) {
            _canvas.Save();
            AntiOrientAxis((_deviceHeight / 2) + 40);
            _canvas.RotateDegrees(90f);
            _canvas.DrawText(label, _xOffset, -_xOffset / 2, paint);
            _canvas.Restore();
        }

        internal override void AntiOrientAxis(float heightSpacing) {
            if (heightSpacing == float.MaxValue) {
                _canvas.Translate(0, _deviceHeight);
                _canvas.Scale(1, -1);
            }
            else {
                _canvas.Translate(0, heightSpacing);
                _canvas.Scale(1, -1);
            }
        }

        private float _deviceHeight;
        private float _deviceWidth;
        private SKCanvas _canvas;
        private float _xOffset;
        private float _yOffset;
    }
}
