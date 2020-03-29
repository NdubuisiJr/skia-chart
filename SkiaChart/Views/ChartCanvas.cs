using SkiaChart.Charts;
using SkiaChart.Helpers;
using SkiaChart.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaChart.Views {
    /// <summary>
    /// The main control for displaying all kinds of charts
    /// </summary>
    /// <typeparam name="T">The type of the chart to render</typeparam>
    public class ChartCanvas<T> : SKCanvasView where T : ChartBase {
        public static readonly BindableProperty ChartProperty = BindableProperty.Create(
            nameof(Chart), typeof(Chart<T>), typeof(ChartCanvas<T>), null);

        /// <summary>
        /// An instance of the Chart class. It is a bindable property
        /// </summary>
        public Chart<T> Chart {
            get => (Chart<T>)GetValue(ChartProperty);
            set {
                SetValue(ChartProperty, value);
            }
        }

        public static readonly BindableProperty GridLineProperty = BindableProperty.Create(
            nameof(GridLines), typeof(int), typeof(ChartCanvas<T>), 0);

        /// <summary>
        /// The number of grid lines to draw on the chart canvas. It is a bindable property
        /// </summary>
        public int GridLines {
            get => (int)GetValue(GridLineProperty);
            set {
                SetValue(GridLineProperty, value);
            }
        }

        public static readonly BindableProperty GridColorProperty = BindableProperty.Create(
            nameof(GridColor), typeof(SKColor), typeof(ChartCanvas<T>), SKColors.Transparent);

        /// <summary>
        /// The color of the grid lines drawn on the chart canvas. It is a bindable property
        /// </summary>
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

            Chart.GridColor = GridColor;
            Chart.Plot(new CanvasWrapper(canvas, chartArea, GridLines, e.Info.Height, e.Info.Width,
                new Converter(chartArea,xOffset,yOffset)));
        }

        private readonly SKPaint _blackPaint = new SKPaint() {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Transparent,
            StrokeWidth = 5,
            IsAntialias = true
        };
    }
}
