using System;

namespace SkiaChart.Exceptions {
    public class ChartAreaNotDefinedException : Exception {
        public ChartAreaNotDefinedException(string message) : base (message) {}
    }
}
