using SkiaChart.Exceptions;
using SkiaSharp;
using System;

namespace SkiaChart.Helpers {
    internal class Converter {
        internal Converter(SKRect rect, float xOffSet, float yOffSet) {
            if (rect==null) {
                throw new ChartAreaNotDefinedException("The SKRect object that defines the chart area can not be null");
            }
            _rect = rect;
            _xOffset = xOffSet;
            _yOffset = yOffSet;
        }

        internal float XValueToRealScale(float pixelValue, float max, float min) {
            var pixelToUse = pixelValue - _xOffset;
            var result = max - ((_rect.Width - pixelToUse) * (max - min) / _rect.Width);
            return (float) Math.Round(result, 2);
        }

        internal float YValueToRealScale(float pixelValue, float max, float min) {
            var pixelTouse = pixelValue - _yOffset;
            double result = max - ((_rect.Height - pixelTouse) * (max - min) / _rect.Height);
            return (float) Math.Round(result, 2);
        }

        internal SKPoint ToPixelScale(SKPoint point, float Xmax, float Xmin, float Ymax, float Ymin) {
            var result = new SKPoint {
                X = _xOffset + ((point.X - Xmin) * _rect.Width / (Xmax - Xmin)),
                Y = (_yOffset + (point.Y - Ymin) * _rect.Height / (Ymax - Ymin))
            };
            return result;
        }

        private SKRect _rect;
        private float _xOffset;
        private float _yOffset;
    }
}
