using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Misc.Root {
	[CreateAssetMenu(menuName = "Santa/Resources")]
	public class Resources : ScriptableObject {
		[SerializeField] private Canvas _canvasPrefab;
		[SerializeField] private List<UiScreenConfig> _screenConfigs;

		public Canvas CanvasPrefab => _canvasPrefab;
		public IReadOnlyList<UiScreenConfig> ScreenConfigs => _screenConfigs;
	}

	[Serializable]
	public struct UiScreenConfig {
		[SerializeField] private UiScreenType _type;
		[SerializeField] private UiScreen _prefab;
		
		public UiScreenType Type => _type;
		public UiScreen Prefab => _prefab;
	}
}