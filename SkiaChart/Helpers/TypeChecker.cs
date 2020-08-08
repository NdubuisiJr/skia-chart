using SkiaChart.Interfaces;

namespace SkiaChart.Helpers {
    public static class TypeChecker {
        public static bool IsSingleValueChart<T>() where T: class {
            return typeof(ISingleValueChart).Equals(typeof(T)
                    .GetInterface("ISingleValueChart"));
        }
    }
}
