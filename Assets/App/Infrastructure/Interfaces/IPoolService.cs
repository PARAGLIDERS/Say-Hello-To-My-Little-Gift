using Pooling;
using System;
using UnityEngine;

public interface IPoolService {
	PoolObject Spawn(PoolType type, Vector3 position, Quaternion rotation,
		Action onActivate = null, Action onDeactivate = null);

	void DeactivateAll();
}

