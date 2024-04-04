using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Pooling {
	[CreateAssetMenu(menuName = Constants.MenuFolder + nameof(PoolsConfig))]
	public class PoolsConfig : ScriptableObject {
		public List<PoolsConfigItem> Items;

		private void OnValidate() {
			HashSet<string> names = new HashSet<string>();	
			foreach (var item in Items) {
				item.Validate();
				if (!names.Add(item.Name)) {
					Debug.LogError($"{item.Name} is already added!");
				}
			}
		}
	}

	[System.Serializable]
	public class PoolsConfigItem {
		[HideInInspector] public string Name;

		public int Size;
		public PoolType Type;
		public PoolObject Prefab;

		public void Validate() {
			Name = Type.ToString();
		}
	}
}
