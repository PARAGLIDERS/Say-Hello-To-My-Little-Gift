using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui {
    [CreateAssetMenu(menuName ="Screen Config")]
    public class UiScreenConfig : ScriptableObject {
        [SerializeField] private List<UiScreenConfigItem> _items;
        public List<UiScreenConfigItem> Items => _items;
    }

	[Serializable]
	public class UiScreenConfigItem {
		[SerializeField] private UiScreenType _type;
		[SerializeField] private UiScreen _prefab;
		
		public UiScreenType Type => _type;
		public UiScreen Prefab => _prefab;
	}
}