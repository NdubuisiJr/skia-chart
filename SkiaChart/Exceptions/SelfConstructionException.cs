using SkiaChart.Charts;
using System;

namespace SkiaChart.Exceptions {
    public class SelfConstructionException : Exception {
        public SelfConstructionException(string  chartName) :
            base($"{chartName} can not self construct. Make sure it is being constructed through " +
                $"{nameof(Chart<ChartBase>)}"){
        }
    }
}
