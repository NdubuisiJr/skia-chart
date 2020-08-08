using SkiaChart.Axes;
using SkiaChart.Interfaces;
using SkiaChart.Models;
using System;

namespace SkiaChart.Charts {
    public class DonutChart : RadialChart, ISingleValueChart {
        public DonutChart(string label, float value) : base(label,value) {}
        public override void RenderChart(CanvasWrapper canvas, Axis axis, IMinMax minMax) {
            throw new NotImplementedException();
        }
    }
}
