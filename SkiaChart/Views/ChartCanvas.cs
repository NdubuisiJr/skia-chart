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
            nameof(Chart), typeof(Chart<T>), typeof(ChartCanvas<T>), null,
            defaultBindingMode: BindingMode.TwoWay, propertyChanged: ChartChanged);

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
            nameof(GridLines), typeof(int), typeof(ChartCanvas<T>), 0, 
            defaultBindingMode: BindingMode.TwoWay, propertyChanged: GridLinesChanged);

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
            nameof(GridColor), typeof(SKColor), typeof(ChartCanvas<T>), SKColors.Transparent,
            defaultBindingMode: BindingMode.TwoWay, propertyChanged: GridColorChanged);

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


        // Show legend or not
        public static readonly BindableProperty CanShowLegendProperty = BindableProperty.Create(
            nameof(CanShowLegend), typeof(bool), typeof(ChartCanvas<T>), false,
            defaultBindingMode: BindingMode.TwoWay, propertyChanged: CanShowLegendChanged);

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

        // Legend items spacing
        public static readonly BindableProperty LegendItemSpacingProperty = BindableProperty.Create(
            nameof(LegendItemSpacing), typeof(float), typeof(ChartCanvas<T>), 40f,
            defaultBindingMode: BindingMode.TwoWay, propertyChanged: LegendItemSpacingChanged);

        private static void LegendItemSpacingChanged(BindableObject bindable,
            object oldValue, object newValue) {
            var canvas = (ChartCanvas<T>)bindable;
            canvas.LegendItemSpacing = (float)newValue;
            canvas.InvalidateSurface();
        }

        /// <summary>
        /// TheSpacing between legend lines. It is a bindable property
        /// </summary>
        public float LegendItemSpacing {
            get => (float)GetValue(LegendItemSpacingProperty);
            set {
                SetValue(LegendItemSpacingProperty, value);
            }
        }


        // Label text size
        public static readonly BindableProperty LabelTextSizeProperty = BindableProperty.Create(
            nameof(LabelTextSize), typeof(float), typeof(ChartCanvas<T>), 15f,
            defaultBindingMode: BindingMode.TwoWay, propertyChanged: LabelTextSizeChanged);

        private static void LabelTextSizeChanged(BindableObject bindable,
            object oldValue, object newValue) {
            var canvas = (ChartCanvas<T>)bindable;
            canvas.LabelTextSize = (float)newValue;
            canvas.InvalidateSurface();
        }

        /// <summary>
        /// TheSpacing between legend lines. It is a bindable property
        /// </summary>
        public float LabelTextSize {
            get => (float)GetValue(LabelTextSizeProperty);
            set {
                SetValue(LabelTextSizeProperty, value);
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e) {
            base.OnPaintSurface(e);
            if (Chart == null) return;

            bool isAndroidOrIOS;
            int heightScaler;
            float bottomMargin;
            switch (Device.RuntimePlatform) {
                case Device.WPF:
                case Device.GTK:
                case Device.macOS:
                case Device.UWP: {
                        isAndroidOrIOS = false;
                        heightScaler = 5;
                        bottomMargin = 40;
                        break;
                    }
                default: {
                        isAndroidOrIOS = true;
                        heightScaler = 3;
                        bottomMargin = CanShowLegend ? heightScaler * ((float)e.Info.Height / 15) : (float)e.Info.Height / 15;
                        break;
                    }
            };

            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);

            var xOffset = (float)e.Info.Width / 15;
            var yOffset = CanShowLegend ? heightScaler * ((float)e.Info.Height / 15) : (float)e.Info.Height / 15;
            var chartArea = new SKRect(xOffset, yOffset, e.Info.Width - (xOffset), e.Info.Height - bottomMargin);
            canvas.DrawRect(chartArea, _blackPaint);
            Chart.GridColor = GridColor;
            Chart.Plot(new CanvasWrapper(canvas, chartArea, GridLines, e.Info.Height, e.Info.Width, isAndroidOrIOS,
                CanShowLegend, LegendItemSpacing, LabelTextSize, new Converter(chartArea, xOffset, yOffset)));
        }

        private readonly SKPaint _blackPaint = new SKPaint() {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Transparent,
            IsAntialias = true
        };
    }
}
