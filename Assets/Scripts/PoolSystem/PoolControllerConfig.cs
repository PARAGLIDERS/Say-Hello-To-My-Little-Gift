using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem {
	[CreateAssetMenu(menuName = "Santa/Pool Controller Config", fileName = "Pool Controller Config")]
	public class PoolControllerConfig : ScriptableObject {
		[SerializeField] private List<PoolConfig> _configs = new();

		public List<PoolConfigItem> GetItems() {
			List<PoolConfigItem> items = new List<PoolConfigItem>();

			foreach (var config in _configs) {
				items.AddRange(config.Items);
			}

			return items;
		}

		private void OnValidate() {
			HashSet<string> names = new HashSet<string>();	
			foreach (var config in _configs) {
				if (!names.Add(config.name)) {
					Debug.LogError($"{config.name} is already added!");
				}
			}
		}
	}
}
