using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem {
	[CreateAssetMenu(menuName = "Santa/Pool Config", fileName = "Pool Config")]
	public class PoolConfig : ScriptableObject {
		[SerializeField] private List<PoolConfigItem> _items = new();
		public List<PoolConfigItem> Items => _items;
		
        // oh no, incapsulation violation :)
		private void OnValidate() {
            foreach (PoolConfigItem item in _items) {
				item.Name = item.Type.ToString();
			}
		}
	}

    [Serializable]
    public class PoolConfigItem {
        [HideInInspector] public string Name;

        [SerializeField] private int _size;
        [SerializeField] private PoolType _type;
        [SerializeField] private PoolObject _prefab;

        public int Size => _size;
        public PoolType Type => _type;
        public PoolObject Prefab => _prefab;
    }
}