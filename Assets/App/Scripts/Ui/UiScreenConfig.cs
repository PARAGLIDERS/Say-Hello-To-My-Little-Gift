using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui {
    [CreateAssetMenu(menuName ="Santa/Ui Screen Config", fileName = "Ui Screen Config")]
    public class UiScreenConfig : ScriptableObject {
        [SerializeField] private List<UiScreenConfigItem> _items;
        public List<UiScreenConfigItem> Items => _items;

        private void OnValidate() {
            foreach (UiScreenConfigItem item in _items) {
				item.Validate();
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

		public void Validate() {
			Name = Type.ToString();
		}
	}
}