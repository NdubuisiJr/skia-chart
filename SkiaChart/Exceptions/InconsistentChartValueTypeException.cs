using System;

namespace SkiaChart.Exceptions {

    public class InconsistentChartValueTypeException : Exception {
        public InconsistentChartValueTypeException(string message) : base(message) { 
        }
    }
}
