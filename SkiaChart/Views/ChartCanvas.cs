using SkiaChart.Charts;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaChart.Views {
    public class ChartCanvas<T> : SKCanvasView where T : IChart {
        public static readonly BindableProperty ChartProperty = BindableProperty.Create(
            nameof(Chart), typeof(Chart<T>), typeof(ChartCanvas<T>), null);
        public Chart<T> Chart {
            get => (Chart<T>)GetValue(ChartProperty);
            set {
                SetValue(ChartProperty, value);
            }
        }

        public static readonly BindableProperty GridLineProperty = BindableProperty.Create(
            nameof(GridLines), typeof(int), typeof(ChartCanvas<T>), 0);
        public int GridLines {
            get => (int)GetValue(GridLineProperty);
            set {
                SetValue(GridLineProperty, value);
            }
        }

        public static readonly BindableProperty GridColorProperty = BindableProperty.Create(
            nameof(GridColor), typeof(SKColor), typeof(ChartCanvas<T>), SKColors.Black);
        public SKColor GridColor {
            get => (SKColor)GetValue(GridColorProperty);
            set {
                SetValue(GridLineProperty, value);
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e) {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);
            var xOffset = (float)e.Info.Width / 15;
            var yOffset = (float)e.Info.Height / 15;
            var chartArea = new SKRect(xOffset, yOffset, e.Info.Width - (xOffset), e.Info.Height - (yOffset));
            canvas.DrawRect(chartArea, _blackPaint);

            Chart.ChartArea = chartArea;
            Chart.XOffset = xOffset;
            Chart.YOffset = yOffset;
            Chart.GridColor = GridColor;
            Chart.Axis.OrientAxis(canvas, e.Info.Width, e.Info.Height);
            Chart.SetGrid(canvas, GridLines);
            Chart.Plot(canvas);
        }

        private SKPaint _blackPaint = new SKPaint() {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Transparent,
            StrokeWidth = 5,
            IsAntialias = true
        };
    }
}
