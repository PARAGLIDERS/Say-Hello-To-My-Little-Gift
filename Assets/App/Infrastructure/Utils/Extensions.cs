using System.Collections.Generic;
using UnityEngine;

namespace Utils {
    public static class Extensions {
        public static Vector3 With(this Vector3 self, float? x = null, float? y = null, float? z = null) {
            return new Vector3(x ?? self.x, y ?? self.y, z ?? self.z);
        }

        public static Color With(this Color self, float? r = null, float? g = null, float? b = null, float? a = null) {
            return new Color(r ?? self.r, g ?? self.g, b ?? self.b, a ?? self.a);
        }

		public static T Random<T>(this List<T> list) {
            return list[UnityEngine.Random.Range(0, list.Count)];
		}
	}
}
