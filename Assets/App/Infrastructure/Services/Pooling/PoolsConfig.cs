using System;
using UnityEngine;
using Utils;

namespace Pooling {
	[CreateAssetMenu(menuName = Constants.MenuFolder + nameof(PoolsConfig))]
	public class PoolsConfig : ItemsConfig<PoolsConfigItem> { }

	[Serializable]
	public class PoolsConfigItem : ItemsConfigItem {
		public override string Name => Type == null ? string.Empty : Type.name;

		public int Size;
		public ObjectType Type;
		public PoolObject Prefab;
	}
}
