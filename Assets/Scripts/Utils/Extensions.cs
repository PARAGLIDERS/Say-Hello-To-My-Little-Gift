using UnityEngine;

namespace Utils {
    public static class Extensions {
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null) {
            Vector3 result = vector;
            
            result.x = x ?? result.x;
            result.y = y ?? result.y;
            result.z = z ?? result.z;

            return result;
        }

        public static Color With(this Color color, float? r = null, float? g = null, float? b = null, float? a = null) {
            Color result = color;

			result.r = r ?? color.r;
			result.g = g ?? color.g;
			result.b = b ?? color.b;
			result.a = a ?? color.a;

            return result;
        }
    }
}
