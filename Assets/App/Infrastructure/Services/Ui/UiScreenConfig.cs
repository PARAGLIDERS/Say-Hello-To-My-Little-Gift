using System;
using UnityEngine;
using Utils;

namespace Ui {
	[CreateAssetMenu(menuName = Constants.MenuFolder + nameof(UiScreenConfig))]
	public class UiScreenConfig : ItemsConfig<UiScreenConfigItem> { }

	[Serializable]
	public class UiScreenConfigItem : ItemsConfigItem {
		public override string Name => Type.ToString();

		public UiScreenType Type;
		public UiScreen Prefab;
	}
}