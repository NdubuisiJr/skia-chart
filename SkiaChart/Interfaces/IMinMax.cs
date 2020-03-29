using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaChart.Interfaces {
    public interface IMinMax {
        float Xmin { get; set; }

        float Ymin { get; set; }

        float Xmax { get; set; }

        float Ymax { get; set; }
    }
}
