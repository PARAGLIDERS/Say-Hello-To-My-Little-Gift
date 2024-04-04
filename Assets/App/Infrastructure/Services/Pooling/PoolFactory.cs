using Pooling;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolFactory {
	private readonly DiContainer _container;
	private readonly Transform _parent;
	private readonly PoolsConfig _config;

	private readonly Dictionary<ObjectType, PoolObject> _data;

	public PoolFactory(DiContainer container, Transform parent, PoolsConfig config) {
		_container = container;
		_parent = parent;
		_config = config;
		_data = new Dictionary<ObjectType, PoolObject>();
	}

	public void Initialize() {
		List<PoolsConfigItem> items = _config.Items;

		for (int i = 0; i < items.Count; i++) {
			PoolsConfigItem item = items[i];
			_data.Add(item.Type, item.Prefab);
		}
	}

	public PoolObject Create(ObjectType poolType) {
		if (!_data.TryGetValue(poolType, out PoolObject prefab)) {
			Debug.LogError($"No such object in pool config: {poolType}");
			return null;
		}

		PoolObject poolObject = _container.InstantiatePrefabForComponent<PoolObject>(prefab, _parent);
		poolObject.Hide();

		return poolObject;
	}

	public Dictionary<ObjectType, Queue<PoolObject>> CreatePools() {
		Dictionary<ObjectType, Queue<PoolObject>> pools = new Dictionary<ObjectType, Queue<PoolObject>>();
		List<PoolsConfigItem> items = _config.Items;

		for (int i = 0; i < items.Count; i++) {
			PoolsConfigItem item = items[i];
			Queue<PoolObject> pool = new Queue<PoolObject>();

			for (int j = 0; j < item.Size; j++) {
				PoolObject poolObject = Create(item.Type);
				pool.Enqueue(poolObject);
			}

			pools.Add(item.Type, pool);
		}

		return pools;
	}
}
