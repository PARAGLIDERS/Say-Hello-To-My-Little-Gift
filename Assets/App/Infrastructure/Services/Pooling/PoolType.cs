using System;
using UnityEngine;
using Utils;

namespace Pooling {
	[CreateAssetMenu(menuName = Constants.MenuFolder + nameof(PoolType))]
	public class PoolType : ScriptableObject {
		public override bool Equals(object obj) {
			return obj is PoolType type &&
				   base.Equals(obj) &&
				   name == type.name;
		}

		public override int GetHashCode() {
			return HashCode.Combine(base.GetHashCode(), name);
		}
	}
}