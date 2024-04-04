using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui {
    [CreateAssetMenu(menuName ="Santa/Ui Screen Config", fileName = "Ui Screen Config")]
    public class UiScreenConfig : ScriptableObject {
        public List<UiScreenConfigItem> Items;

        private void OnValidate() {
            foreach (UiScreenConfigItem item in Items) {
				item.Validate();
            }
        }
    }

	[Serializable]
	public class UiScreenConfigItem {
        [HideInInspector] public string Name;

		public UiScreenType Type;
		public UiScreen Prefab;
		
		public void Validate() {
			Name = Type.ToString();
		}
	}
}