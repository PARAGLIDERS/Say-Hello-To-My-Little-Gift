using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem {
	[CreateAssetMenu(menuName = "Pool Config")]
	public class PoolConfig : ScriptableObject {
		[SerializeField] private List<Pool> _pools = new();
		public List<Pool> Pools => _pools;
		
		private void OnValidate() {
			foreach (Pool pool in _pools) {
				pool.Name = pool.Type.ToString();
			}
		}
	}
}