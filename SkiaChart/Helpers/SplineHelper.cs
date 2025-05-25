
using System.Collections.Generic;
using SkiaSharp;

namespace SkiaChart.Helpers
{
    public static class SplineHelper
    {
        // tension = 0.5 is standard Catmull-Rom; lower is tighter, higher is looser
        public static SKPath CreateCatmullRomSpline(IList<SKPoint> points, float tension = 0.5f, bool closed = false, int segments = 16)
        {
            var path = new SKPath();

            if (points == null || points.Count < 2)
                return path;

            path.MoveTo(points[0]);

            for (int i = 0; i < points.Count - 1; i++)
            {
                SKPoint p0 = i > 0 ? points[i - 1] : points[i];
                SKPoint p1 = points[i];
                SKPoint p2 = points[i + 1];
                SKPoint p3 = i < points.Count - 2 ? points[i + 2] : p2;

                for (int j = 1; j <= segments; j++)
                {
                    float t = j / (float)segments;
                    SKPoint point = CalculateCatmullRomPoint(t, p0, p1, p2, p3, tension);
                    path.LineTo(point);
                }
            }

            if (closed)
            {
                path.Close();
            }

            return path;
        }

        private static SKPoint CalculateCatmullRomPoint(float t, SKPoint p0, SKPoint p1, SKPoint p2, SKPoint p3, float tension)
        {
            float t2 = t * t;
            float t3 = t2 * t;

            float a0 = -tension * t3 + 2 * tension * t2 - tension * t;
            float a1 = (2 - tension) * t3 + (tension - 3) * t2 + 1;
            float a2 = (tension - 2) * t3 + (3 - 2 * tension) * t2 + tension * t;
            float a3 = tension * t3 - tension * t2;

            float x = a0 * p0.X + a1 * p1.X + a2 * p2.X + a3 * p3.X;
            float y = a0 * p0.Y + a1 * p1.Y + a2 * p2.Y + a3 * p3.Y;

            return new SKPoint(x, y);
        }
    }

}