using System;
using System.Collections.Generic;
using Player;
using PoolSystem;
using Ui;
using UnityEngine;

namespace Misc.Root {
	[CreateAssetMenu(menuName = "Santa/Resources")]
	public class Resources : ScriptableObject {
		[SerializeField] private PoolConfig _poolConfig;
		[SerializeField] private Canvas _canvasPrefab;
		[SerializeField] private List<UiScreenConfig> _screenConfigs;

		public Canvas CanvasPrefab => _canvasPrefab;
		public List<UiScreenConfig> ScreenConfigs => _screenConfigs;
		public List<Pool> Pools => _poolConfig.Pools;
	}
}