using System.Collections.Generic;
using SkiaChart.Charts;
using SkiaSharp;

namespace SkiaChart.Axes {
    public class NormalXYAxis<T> : Axis<T> where T: IChart {
        public NormalXYAxis(IEnumerable<T> lineSeries) : base(lineSeries) {
        }

        internal override void OrientAxis(SKCanvas canvas, float width, float height) {
            _height = height;
            _width = width;
            _canvas = canvas;

            canvas.Translate(0, height);
            canvas.Scale(1, -1);
        }


        internal override void PositionXLabel(string label, float widthSpacing, float bottomOrTop, SKPaint paint) {
            _canvas.Save();
            AntiOrientAxis(float.MaxValue);
            _canvas.DrawText(label, widthSpacing, bottomOrTop+20, paint);
            _canvas.Restore();
        }

        internal override void PositionYLabel(string label, float heightSpacing, float rightOrLeft, SKPaint paint) {
            _canvas.Save();
            AntiOrientAxis(heightSpacing);
            _canvas.DrawText(label, rightOrLeft - 20, 0, paint);
            _canvas.Restore();
        }

        private  void AntiOrientAxis(float heightSpacing) {
            if (heightSpacing==float.MaxValue) {
                _canvas.Translate(0, _height);
                _canvas.Scale(1, -1);
            }
            else {
                _canvas.Translate(0, heightSpacing);
                _canvas.Scale(1, -1);
            }
        }

        private float _height;
        private float _width;
        private SKCanvas _canvas;
    }
}
