using PoolSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui {
    [CreateAssetMenu(menuName ="Screen Config")]
    public class UiScreenConfig : ScriptableObject {
        [SerializeField] private List<UiScreenConfigItem> _items;
        public List<UiScreenConfigItem> Items => _items;

        // oh no, incapsulation violation :)
        private void OnValidate() {
            foreach (UiScreenConfigItem item in _items) {
                item.Name = item.Type.ToString();
            }
        }
    }

	[Serializable]
	public class UiScreenConfigItem {
        [HideInInspector] public string Name;

		[SerializeField] private UiScreenType _type;
		[SerializeField] private UiScreen _prefab;
		
		public UiScreenType Type => _type;
		public UiScreen Prefab => _prefab;
	}
}