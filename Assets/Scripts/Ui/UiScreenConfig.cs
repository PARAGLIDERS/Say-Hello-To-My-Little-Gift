using System;
using UnityEngine;

namespace Ui {
	[Serializable]
	public struct UiScreenConfig {
		[SerializeField] private UiScreenType _type;
		[SerializeField] private UiScreen _prefab;
		
		public UiScreenType Type => _type;
		public UiScreen Prefab => _prefab;
	}
}