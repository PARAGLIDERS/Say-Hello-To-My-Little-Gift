using System;
using UnityEngine;
using Utils;

[CreateAssetMenu(menuName = Constants.MenuFolder + nameof(ObjectType))]
public class ObjectType : ScriptableObject {
	public override bool Equals(object obj) {
		return obj is ObjectType type &&
				base.Equals(obj) &&
				name == type.name;
	}

	public override int GetHashCode() {
		return HashCode.Combine(base.GetHashCode(), name);
	}
}
