using SkiaChart.Charts;
using SkiaChart.Helpers;
using SkiaChart.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;

namespace SkiaChart.Views {
    /// <summary>
    /// The main control for displaying all kinds of charts
    /// </summary>
    /// <typeparam name="T">The type of the chart to render</typeparam>
    public class ChartCanvas<T> : SKCanvasView where T : ChartBase {
        public static readonly BindableProperty ChartProperty = BindableProperty.Create(
            nameof(Chart), typeof(Chart<T>), typeof(ChartCanvas<T>), null,propertyChanged:ChartChanged);

        private static void ChartChanged(BindableObject bindable, 
            object oldValue, object newValue) {
            var canvas = (ChartCanvas<T>)bindable;
            canvas.Chart = (Chart<T>)newValue;
            canvas.InvalidateSurface();
        }

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
            nameof(GridLines), typeof(int), typeof(ChartCanvas<T>), 0,propertyChanged:GridLinesChanged);

        private static void GridLinesChanged(BindableObject bindable, 
            object oldValue, object newValue) {
            var canvas = (ChartCanvas<T>)bindable;
            canvas.GridLines = (int)newValue;
            canvas.InvalidateSurface();
        }

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
            nameof(GridColor), typeof(SKColor), typeof(ChartCanvas<T>), SKColors.Transparent,propertyChanged:GridColorChanged);

        private static void GridColorChanged(BindableObject bindable, 
            object oldValue, object newValue) {
            var canvas = (ChartCanvas<T>)bindable;
            canvas.GridColor = (SKColor)newValue;
            canvas.InvalidateSurface();
        }

        /// <summary>
        /// The color of the grid lines drawn on the chart canvas. It is a bindable property
        /// </summary>
        public SKColor GridColor {
            get => (SKColor)GetValue(GridColorProperty);
            set {
                SetValue(GridLineProperty, value);
            }
        }

        public static readonly BindableProperty CanShowLegendProperty = BindableProperty.Create(
            nameof(CanShowLegend), typeof(bool), typeof(ChartCanvas<T>), false,propertyChanged:CanShowLegendChanged);

        private static void CanShowLegendChanged(BindableObject bindable,
            object oldValue, object newValue) {
            var canvas = (ChartCanvas<T>)bindable;
            canvas.CanShowLegend = (bool)newValue;
            canvas.InvalidateSurface();
        }

        /// <summary>
        /// The color of the grid lines drawn on the chart canvas. It is a bindable property
        /// </summary>
        public bool CanShowLegend {
            get => (bool)GetValue(CanShowLegendProperty);
            set {
                SetValue(CanShowLegendProperty, value);
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e) {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);
            var xOffset = (float)e.Info.Width / 15;
            var yOffset =CanShowLegend? 3*((float)e.Info.Height / 15):(float)e.Info.Height / 15;
            var chartArea = new SKRect(xOffset, yOffset, e.Info.Width - (xOffset), e.Info.Height - (yOffset));
            canvas.DrawRect(chartArea, _blackPaint);
            if (Chart == null) return;
            Chart.GridColor = GridColor;
            Chart.Plot(new CanvasWrapper(canvas, chartArea, GridLines, e.Info.Height, e.Info.Width, CanShowLegend,
                new Converter(chartArea,xOffset,yOffset)));
        }

        private readonly SKPaint _blackPaint = new SKPaint() {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Transparent,
            IsAntialias = true
        };
    }
}
