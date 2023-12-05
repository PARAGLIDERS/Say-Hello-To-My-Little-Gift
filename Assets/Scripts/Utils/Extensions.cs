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
    }
}
